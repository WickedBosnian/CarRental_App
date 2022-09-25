using CarRental_Domain.Entities;
using CarRental_DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental_Infrastructure.Helpers
{
    public static class Mapper
    {
        public static ClientDTO ToClientDTO(Client client)
        {   
            return new ClientDTO()
            {
                ClientId = client.ClientId,
                Firstname = client.Firstname,
                Lastname = client.Lastname,
                DriverLicenceNumber = client.DriverLicenceNumber,
                PersonalIdcardNumber = client.PersonalIdcardNumber,
                Birthdate = client.Birthdate,
                Gender = client.Gender
            };
        }

        public static VehicleTypeDTO ToVehicleTypeDTO(VehicleType vehicleType)
        {
            return new VehicleTypeDTO()
            {
                VehicleTypeId = vehicleType.VehicleTypeId,
                VehicleTypeDescription = vehicleType.VehicleTypeDescription,
                VehicleTypeName = vehicleType.VehicleTypeName
            };
        }

        public static VehicleManufacturerDTO ToVehicleManufacturereDTO(VehicleManufacturer vehicleManufacturer)
        {
            return new VehicleManufacturerDTO()
            {
                VehicleManufacturerId = vehicleManufacturer.VehicleManufacturerId,
                VehicleManufacturerDescription = vehicleManufacturer.VehicleManufacturerDescription,
                VehicleManufacturerName = vehicleManufacturer.VehicleManufacturerName
            };
        }

        public static VehicleDTO ToVehicleDTO(Vehicle vehicle)
        {
            return new VehicleDTO()
            {
                VehicleId = vehicle.VehicleId,
                VehicleName = vehicle.VehicleName,
                Color = vehicle.Color,
                DateManufactured = vehicle.DateManufactured,
                PricePerDay = vehicle.PricePerDay,
                VehicleTypeId = vehicle.VehicleTypeId,
                VehicleManufacturerId = vehicle.VehicleManufacturerId,
                VehicleManufacturer = vehicle.VehicleManufacturer != null ? ToVehicleManufacturereDTO(vehicle.VehicleManufacturer) : null,
                VehicleType = vehicle.VehicleType != null ? ToVehicleTypeDTO(vehicle.VehicleType) : null
            };
        }

        public static ReservationDTO ToReservationDTO(Reservation reservation)
        {
            return new ReservationDTO()
            {
                ReservationId = reservation.ReservationId,
                ReservationDateFrom = reservation.ReservationDateFrom,
                ReservationDateTo = reservation.ReservationDateTo,
                Active = reservation.Active,
                ClientId = reservation.ClientId,
                VehicleId = reservation.VehicleID,
                Client = reservation.Client != null ? ToClientDTO(reservation.Client) : null,
                Vehicle = reservation.Vehicle != null ? ToVehicleDTO(reservation.Vehicle) : null
            };
        }
    }
}
