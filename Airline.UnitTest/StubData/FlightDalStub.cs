using Airline.UnitTest;
using Airline.UnitTest.ExceptionHandling;
using ClassLib.Data;
using ClassLib.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Airline.UnitTest.StubData
{
    public class FlightDalStub : FitchData<FlightObject>, PersistData<FlightObject>
    {
        private List<FlightObject> flightDtoList = new List<FlightObject>()
        {
        new FlightObject(){  aircraftCode="FD453", aircraftType="A320", departureCountry="Netherlands", arrivalCountry="Japan", departureDate=new DateTime(2008, 5, 3, 6, 30, 00), arrivalDate=new DateTime(2008, 5, 1, 19, 45, 00), flightStatus=true, price=450},
        new FlightObject(){  aircraftCode="FD356", aircraftType="747", departureCountry="Netherlands", arrivalCountry="Vietnam", departureDate=new DateTime(2008, 5, 3, 6, 30, 00), arrivalDate=new DateTime(2008, 5, 3, 16, 40, 00), flightStatus=true, price=380},
        new FlightObject(){  aircraftCode="FD596", aircraftType="A321", departureCountry="Netherlands", arrivalCountry="Italy", departureDate=new DateTime(2008, 7, 18, 7, 30, 00), arrivalDate=new DateTime(2008, 7, 18, 9, 05, 00), flightStatus=false, price= 72}
        };

        public void delete(int id) => flightDtoList.RemoveAt(id);
        public IEnumerable<FlightObject> getAll() => flightDtoList;

        public FlightObject getById(int id)
        {
            FlightObject flight = flightDtoList.Single(x => x.flightId == id);
            return flight;
        }

        public void save(FlightObject flight)
        {
            if (flight.aircraftCode == null || flight.aircraftType == null || flight.departureCountry == null || flight.arrivalCountry == null || flight.departureDate < DateTime.Now || flight.arrivalDate < DateTime.Now)
            {
                throw new FlightAddException(string.Format("Unable to find a customer by matching password"));
            }
            flightDtoList.Add(flight);
        }
        public void update(FlightObject flight)
        {
            int index = flightDtoList.FindLastIndex(c => c.flightId == flight.flightId);
            if (index != -1)
            {
                flightDtoList[index] = new FlightObject()
                {
                    flightId = flight.flightId,
                    aircraftCode = flight.aircraftCode,
                    aircraftType = flight.aircraftType,
                    departureCountry = flight.departureCountry,
                    arrivalCountry = flight.arrivalCountry,
                    departureDate = flight.departureDate,
                    arrivalDate = flight.arrivalDate,
                    flightStatus = flight.flightStatus,
                    price = flight.price
                };
            }
        }


        public int saveGetId(FlightObject t)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FlightObject> search(string searchString)
        {
            throw new NotImplementedException();
        }


        public FlightObject verifyLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FlightObject> getAllByCustomer(int id)
        {
            throw new NotImplementedException();
        }
    }
}

