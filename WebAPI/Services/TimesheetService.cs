﻿using System;
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
        public async Task CreateTimesheet(Guid positionId, TimesheetCreateDTO dto)
        {
            var position = await _positionRepository.GetPositionById(positionId);
            if (position == null)
            {
                throw new Exception("There is no position with this id.");
            }

            var timesheet = _timesheetDtoConverter.TimesheetCreateDtoToTimesheet(dto, position);
            await _timesheetRepository.CreateTimesheet(timesheet);
        }

        // Get all timesheet Dar um return de dto 
        public async Task<IEnumerable<Timesheet>> GetAllTimesheets()
        {
            return await _timesheetRepository.GetAllTimesheets();
        }

        // Get a timesheet by ID
        public async Task<Timesheet> GetTimesheetById(Guid id)
        {
            var timesheet = await _timesheetRepository.GetTimesheetById(id);
            if (timesheet == null)
            {
                throw new Exception("There is no timesheet with this Id.");
            }

            return timesheet;
        }

        // Update a timesheet
        public async Task UpdateTimesheet(Guid timesheetId,Timesheet timesheet)
        {
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