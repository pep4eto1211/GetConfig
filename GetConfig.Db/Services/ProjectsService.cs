using AutoMapper;
using GetConfig.Db.Flags;
using GetConfig.Db.Models;
using GetConfig.Db.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GetConfig.Db.Services
{
    public class ProjectsService : IDisposable
    {
        #region Private fields
        private IMapper _mapper;
        private GetConfigDbEntities _db;
        #endregion

        #region Initialization
        public ProjectsService()
        {
            this.InitializeDbContext();
            this.InitializeAutoMapper();
        }

        private void InitializeDbContext()
        {
            this._db = new GetConfigDbEntities();
        }

        private void InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Project, ProjectViewModel>()
                .ForMember(dest => dest.ValueCount, m => m.MapFrom(c => c.ConfigValues.Count));
                cfg.CreateMap<ConfigValue, ConfigValueViewModel>();
                cfg.CreateMap<ProjectsUser, UserAccessViewModel>()
                .ForMember(dest => dest.UserEmail, m => m.MapFrom(c => this._db.AspNetUsers.Where(e => e.Id == c.UserId).Select(e => e.Email).SingleOrDefault()));
                cfg.CreateMap<Project, ProjectSettingsViewModel>();
            });

            this._mapper = config.CreateMapper();
        }
        #endregion

        #region Public methods
        public List<ProjectViewModel> GetProjectsForUser(string userId)
        {
            var projects = this._db.Projects.Where(e => e.OwnerId == userId).ToList();
            List<ProjectViewModel> projectsViewModels = new List<ProjectViewModel>();
            foreach (var item in projects)
            {
                var newProject = this._mapper.Map<ProjectViewModel>(item);
                newProject.IsShared = false;
                projectsViewModels.Add(newProject);
            }

            var sharedProjects = this._db.ProjectsUsers.Where(e => e.UserId == userId).Select(e => e.Project).ToList();
            foreach (var item in sharedProjects)
            {
                var newProject = this._mapper.Map<ProjectViewModel>(item);
                newProject.IsShared = true;
                projectsViewModels.Add(newProject);
            }

            return projectsViewModels;
        }

        public void CreateConfigValue(string name, string description, string value, int projectId)
        {
            ConfigValue newValue = new ConfigValue()
            {
                Name = name,
                Description = description,
                Value = value,
                ProjectId = projectId
            };

            this._db.ConfigValues.Add(newValue);
            this._db.SaveChanges();
        }

        public void DeleteConfigValue(int id)
        {
            var configValue = this._db.ConfigValues.Where(e => e.Id == id).SingleOrDefault();
            this._db.ConfigValues.Remove(configValue);
            this._db.SaveChanges();
        }

        public void EditConfigValue(string name, string description, string value, int id)
        {
            var configValue = this._db.ConfigValues.Where(e => e.Id == id).SingleOrDefault();
            configValue.Name = name;
            configValue.Description = description;
            configValue.Value = value;

            this._db.SaveChanges();
        }

        public ConfigViewModel GetConfigValuesForProject(int projectId, string userId)
        {
            var project = this._db.Projects.Where(e => e.Id == projectId).SingleOrDefault();

            ConfigViewModel configViewModel = new ConfigViewModel();
            configViewModel.ConfigValues = this.GetConfigValuesViewModels(project);
            configViewModel.ProjectSettings = this.GetProjectSettingsViewModel(project);
            configViewModel.AccessRights = this.GetUserAccessRightsForProject(userId, projectId);
            return configViewModel;
        }
        #endregion

        #region Helper methods
        private List<ConfigValueViewModel> GetConfigValuesViewModels(Project project)
        {
            List<ConfigValueViewModel> valuesViewModels = new List<ConfigValueViewModel>();
            foreach (ConfigValue item in project.ConfigValues)
            {
                valuesViewModels.Add(this._mapper.Map<ConfigValueViewModel>(item));
            }

            return valuesViewModels;
        }

        private ProjectSettingsViewModel GetProjectSettingsViewModel(Project project)
        {
            ProjectSettingsViewModel projectSettingsViewModel = this._mapper.Map<ProjectSettingsViewModel>(project);
            projectSettingsViewModel.UserAccesses = new List<UserAccessViewModel>();
            foreach (var item in project.ProjectsUsers)
            {
                projectSettingsViewModel.UserAccesses.Add(this._mapper.Map<UserAccessViewModel>(item));
            }

            return projectSettingsViewModel;
        }

        private UserAccessRights GetUserAccessRightsForProject(string userId, int projectId)
        {
            UserAccessRights rights;
            var project = this._db.Projects.Where(e => e.Id == projectId).SingleOrDefault();
            if (project.OwnerId == userId)
            {
                rights = UserAccessRights.ViewName | UserAccessRights.AddNew | UserAccessRights.Delete | UserAccessRights.Edit | UserAccessRights.ProjectLevel | UserAccessRights.ViewValue;
            }
            else
            {
                var userAccess = project.ProjectsUsers.Where(e => e.UserId == userId).SingleOrDefault();
                rights = (UserAccessRights)userAccess.AccessFlag;
            }
            return rights;
        }
        #endregion

        #region IDisposable methods
        public void Dispose()
        {
            if (this._db != null)
            {
                this._db.Dispose();
            }
        }
        #endregion
    }
}
