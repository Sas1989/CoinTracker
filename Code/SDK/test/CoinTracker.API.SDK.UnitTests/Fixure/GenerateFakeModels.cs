using API.SDK.UnitTests.Fixure.Models;
using API.UnitTest.Utility.FixtureManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.SDK.UnitTests.Fixure
{
    internal static class GenerateFakeModels
    {
        private static FixureManger fixureManger = new FixureManger();
        public static FakeEntity GenerateEntity()
        {
            return fixureManger.Create<FakeEntity>();
        }

        public static IEnumerable<FakeEntity> GenerateEntityList()
        {
            return fixureManger.CreateList<FakeEntity>();
        }

        public static FakeDto GenerateDto()
        {
            return fixureManger.Create<FakeDto>();
        }

        public static IEnumerable<FakeDto> GenerateDtoList()
        {
            return fixureManger.CreateList<FakeDto>();
        }
        public static FakeRecivedDto GenerateRecivedDto()
        {
            return fixureManger.Create<FakeRecivedDto>();
        }

        public static IEnumerable<FakeRecivedDto?> GenerateRecivedDtoList()
        {
            return fixureManger.CreateList<FakeRecivedDto?>();
        }
    }
}
