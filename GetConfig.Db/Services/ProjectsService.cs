using AutoMapper;
using GetConfig.Db.Models;
using GetConfig.Db.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
                projectsViewModels.Add(this._mapper.Map<ProjectViewModel>(item));
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

        public List<ConfigValueViewModel> GetConfigValuesForProject(int projectId, out string projectName)
        {
            var project = this._db.Projects.Where(e => e.Id == projectId).SingleOrDefault();
            projectName = project.Name;
            List<ConfigValueViewModel> valuesViewModels = new List<ConfigValueViewModel>();
            foreach (ConfigValue item in project.ConfigValues)
            {
                valuesViewModels.Add(this._mapper.Map<ConfigValueViewModel>(item));
            }
            return valuesViewModels;
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
