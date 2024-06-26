using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Dtos.Position;
using Common.Dtos.Timesheet;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.Services
{
    public class TimesheetService
    {
        private readonly ITimesheetRepository _timesheetRepository;
        private readonly IPositionRepository _positionRepository;
        private readonly TimesheetDTOConverter _timesheetDtoConverter;

        public TimesheetService(ITimesheetRepository timesheetRepository, IPositionRepository positionRepository,TimesheetDTOConverter timesheetDtoConverter)
        {
            _timesheetRepository = timesheetRepository;
            _positionRepository = positionRepository;
            _timesheetDtoConverter = timesheetDtoConverter;
        }

        // Create a new position falta timesheets
        public async Task CreateTimesheets(Guid positionId, TimesheetCreateDTO dto)
        {
            var position = await _positionRepository.GetPositionById(positionId);
            if (position == null)
            {
                throw new Exception("There is no position with this id.");
            }

            foreach (var timesheet in dto.TimesheetDtos)
            {
                var newTimesheet = _timesheetDtoConverter.TimesheetCreateDtoToTimesheet(timesheet, positionId);
                await _timesheetRepository.CreateTimesheet(newTimesheet);
            }
        }

        // Get all timesheet Dar um return de dto 
        public async Task<IEnumerable<TimesheetResponseDTO>> GetAllTimesheets()
        {
            var result = new List<TimesheetResponseDTO>();
            var timesheets = await _timesheetRepository.GetAllTimesheets();
            foreach (var timesheet in timesheets)
            {
                result.Add(_timesheetDtoConverter.TimesheetToTimesheetResponseDTO(timesheet));
            }

            return result;
        }

        // Get a timesheet by ID
        public async Task<TimesheetResponseDTO> GetTimesheetById(Guid id)
        {
            var timesheet = await _timesheetRepository.GetTimesheetById(id);
            if (timesheet == null)
            {
                throw new Exception("There is no timesheet with this Id.");
            }

            return _timesheetDtoConverter.TimesheetToTimesheetResponseDTO(timesheet);
        }

        // Update a timesheet
        public async Task UpdateTimesheet(Guid timesheetId,TimesheetUpdateDTO dto)
        {
            var timesheet = await _timesheetRepository.GetTimesheetById(timesheetId);
            if (timesheet == null)
            {
                throw new Exception("There is no timesheet with this Id.");
            }

            timesheet.Date = dto.Date;
            timesheet.TaskDescription = dto.TaskDescription;
            timesheet.HoursWorked = dto.HoursWorked;
            await _timesheetRepository.UpdateTimesheet(timesheet);
        }

        // Delete a timesheet
        public async Task<bool> DeleteTimesheet(Guid id)
        {
            var timesheet = await _timesheetRepository.GetTimesheetById(id);
            if (timesheet == null)
            {
                return false;
            }

            await _timesheetRepository.DeleteTimesheet(timesheet);
            return true;
        }
    }
}