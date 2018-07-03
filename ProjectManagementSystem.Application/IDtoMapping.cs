using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementSystem
{
    internal interface IDtoMapping
    {
        /// <summary>
        /// 实现该接口以进行映射规则创建
        /// </summary>
        void CreateMapping(IMapperConfigurationExpression mapperConfig);
    }
}
