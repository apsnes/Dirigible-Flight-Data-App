using FlightApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAppTests.RepositoryTests
{
    public class FlightApiRepositoryTests
    {
        private FlightApiRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new FlightApiRepository();
        }
    }
}