using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAW.UnitTest.Core
{
    public class AutoMapping
    {
        public IMapper GetMapper(Profile profile)
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(profile);
            });
            var mapper = mockMapper.CreateMapper();
            return mapper;
        }
    }

}
