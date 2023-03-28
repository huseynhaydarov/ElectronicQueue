using AutoMapper;
using Queue.Application.Common.Interfaces;
using Queue.Application.Common.Interfaces.Repositories;
using Queue.Application.RequestModels.QueueFoeServiceRequestModels;
using Queue.Application.RequestModels.ServiceRequestModels;
using Queue.Application.ResponseModels.ServiceResponseModels;
using Queue.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue.Application.Services
{
    public class ServicesService : BaseService<Service, ServiceResponseModel, ServiceRequestModel>, IServicesService
    {
        private IRepository<Service> repository;
        private IMapper mapper;

        public ServicesService(IRepository<Service> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public override ServiceResponseModel Create(ServiceRequestModel services)
        {
            if (services == null) throw new ArgumentNullException(nameof(Service));

            var createServiceRequest = services as CreateServiceRequestModel;
            var entity = mapper.Map<CreateServiceRequestModel, Service>(createServiceRequest);
            repository.Add(entity);
            repository.SaveChanges();
            return mapper.Map<Service, ServiceResponseModel>(entity);
        }

        public override ServiceResponseModel Get(ulong id)
        {
            return mapper.Map<Service, GetServiceResponseModel>(repository.GetById(id));
        }
        public override IEnumerable<ServiceResponseModel> GetAll()
        {
            var service = repository.GetAll();

            return mapper.Map<IEnumerable<GetServiceResponseModel>>(service);
        }
        public override ServiceResponseModel Update(ServiceRequestModel request, ulong id)
        {
            var service = repository.GetById(id);
            if (service == null) throw new ArgumentException(nameof(Service));
            var updateServiceRequest = request as UpdateServiceRequestModel;
            mapper.Map<UpdateServiceRequestModel, Service>(updateServiceRequest);
            repository.Update(service, id);
            repository.SaveChanges();
            return mapper.Map<Service, UpdateServiceResponseModel>(service);
        }
        public override bool Delete(ulong id)
        {
            var result = repository.GetById(id);
            if (result != null)
            {
                repository.Delete(result);
                repository.SaveChanges();
                return true;
            }
            return false;
        }
    }
}












        /*
        public override ServiceResponseModel Create(CreateServiceRequestModel entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(Service));

            var service = mapper.Map<ServiceRequestModel, Service>(entity);
            repository.Add(service);
            repository.SaveChanges();
            return mapper.Map<Service, ServiceResponseModel>(service);

        }

        public override ServiceResponseModel Update(CreateServiceRequestModel entity, ulong id)
        {
            return base.Update(entity, id);
        }

        public override ServiceResponseModel Get(ulong id)
        {
            return mapper.Map<Service, ServiceResponseModel>(repository.GetById(id));
        }

        //public override ServiceResponseModel GetAll(Service entity)
        //{
        //    return mapper.Map<Service, ServiceResponseModel>(repository.GetAll(entity));
        //}

        public override bool Delete(ulong id)
        {
            var result = repository.GetById(id);
            if (result != null)
            {
                repository.Delete(result);
                repository.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
        */