using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;

namespace DevIO.App.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IMapper _mapper;

        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected T mapear<T>(object objeto)
        {
            return _mapper.Map<T>(objeto);
        }

        protected IEnumerable<T1> mapearLista<T1>(object objeto)
        {
            return _mapper.Map<IEnumerable<T1>>(objeto);
        }

    }
}
