using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Dtos.Position;
using ESOF.WebApp.DBLayer.Context;
using ESOF.WebApp.DBLayer.Entities;
using ESOF.WebApp.WebAPI.Repositories;
using ESOF.WebApp.WebAPI.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ESOF.WebApp.Services
{
    public class PositionService
    {
        private readonly IPositionRepository _positionRepository;
        private readonly IJobRepository _jobRepository;
        private readonly PositionDTOConverter _positionDtoConverter;

        public PositionService(IPositionRepository positionRepository, IJobRepository jobRepository, PositionDTOConverter positionDtoConverter)
        {
            _positionRepository = positionRepository;
            _jobRepository = jobRepository;
            _positionDtoConverter = positionDtoConverter;
        }

        // Create a new position falta timesheets
        public async Task CreatePosition(Guid jobId, PositionCreateDTO dto)
        {
            var job = await _jobRepository.GetJobByIdAsync(jobId);
            if (job == null)
            {
                throw new Exception("There is no job with this id.");
            }

            var position = _positionDtoConverter.PositionCreateDTOToPosition(dto, jobId);
            await _positionRepository.CreatePosition(position);
        }

        // Get all positions Dar um return de dto 
        public async Task<IEnumerable<Position>> GetAllPositions()
        {
            return await _positionRepository.GetAllPositions();
        }

        // Get a position by ID
        public async Task<Position> GetPositionById(Guid id)
        {
            var position = await _positionRepository.GetPositionById(id);
            if (position == null)
            {
                throw new Exception("There is no position with this Id.");
            }

            return position;
        }

        // Update a position
        public async Task UpdatePosition(Guid positionId, PositionUpdateDTO dto)
        {
            var position = await _positionRepository.GetPositionById(positionId);
            if (position == null)
            {
                throw new Exception("There is no position with this Id.");
            }

            position.StartDate = dto.StartDate;
            position.EndDate = dto.EndDate;
            position.BillingType = dto.BillingType;
            await _positionRepository.UpdatePosition(position);
        }

        // Delete a position
        public async Task<bool> DeletePosition(Guid id)
        {
            var position = await _positionRepository.GetPositionById(id);
            if (position == null)
            {
                return false;
            }

            await _positionRepository.DeletePosition(position);
            return true;
        }
    }
}