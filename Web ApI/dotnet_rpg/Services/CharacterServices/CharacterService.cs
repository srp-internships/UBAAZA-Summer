global using AutoMapper;
using dotnet_rpg.Dtosa.Charater;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Services.CharacterServices
{
    public class CharacterService : ICharacterService
    {
   
        private readonly IMapper mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper,DataContext context) 
        {
            this.mapper = mapper;
             _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse=new ServiceResponse<List<GetCharacterDto>>();
            var character = mapper.Map<Character>(newCharacter);
           
            _context.Characters.Add(character);
            

            serviceResponse.Data=
               await _context.Characters.Select(c=>mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharater(int Id)
        {

            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == Id);
                if (character is null)
                {
                    throw new Exception($"Character with Id '{Id}' not found. ");
                }
               _context.Characters.Remove(character);
                _context.SaveChangesAsync();
                  serviceResponse.Data =
                   await _context.Characters.Select(c => mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }
       
        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
          
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data=dbCharacters.Select(c => mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data =mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
                var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = 
                  await _context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
               if(character is null)
                {
                    throw new Exception($"Character with Id '{updateCharacter.Id}' not found. ");
                }
               mapper.Map(updateCharacter,character);
                character.Name = updateCharacter.Name;
                character.HitPoints = updateCharacter.HitPoints;
                character.Strength = updateCharacter.Strength;
                character.Defense = updateCharacter.Defense;
                character.Intelligence = updateCharacter.Intelligence;
                character.Class = updateCharacter.Class;
                await _context.SaveChangesAsync();
                serviceResponse.Data = mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponse.Success=false;
                serviceResponse.Message=ex.Message;
            }
                return serviceResponse;
        }
    }
}
