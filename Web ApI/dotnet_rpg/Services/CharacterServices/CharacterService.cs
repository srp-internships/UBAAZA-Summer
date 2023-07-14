global using AutoMapper;
using dotnet_rpg.Dtosa.Charater;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace dotnet_rpg.Services.CharacterServices
{
    public class CharacterService : ICharacterService
    {
   
        private readonly IMapper mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper,DataContext context,IHttpContextAccessor httpContextAccessor ) 
        {
            this.mapper = mapper;
             _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponse=new ServiceResponse<List<GetCharacterDto>>();
            var character = mapper.Map<Character>(newCharacter);
           character.User=await _context.Users.FirstOrDefaultAsync(u=>u.Id==GetUserId());
            _context.Characters.Add(character);
            

            serviceResponse.Data=
               await _context.Characters
               .Where(c=>c.User!.Id==GetUserId())
               .Select(c=>mapper.Map<GetCharacterDto>(c))
               .ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharater(int Id)
        {

            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == Id && c.User!.Id==GetUserId());
                if (character is null)
                {
                    throw new Exception($"Character with Id '{Id}' not found. ");
                }
               _context.Characters.Remove(character);
                _context.SaveChangesAsync();
                  serviceResponse.Data =
                   await _context.Characters
                   .Where(c=> c.User!.Id==GetUserId())
                   .Select(c => mapper.Map<GetCharacterDto>(c)).ToListAsync();
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
          
            var dbCharacters = await _context.Characters
                 .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c=>c.User!.Id==GetUserId())
                .ToListAsync();
            serviceResponse.Data=dbCharacters.Select(c => mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters
                .Include(c=>c.Weapon)
                .Include(c=>c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id==GetUserId());
            serviceResponse.Data =mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
                var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = 
                  await _context.Characters
                  .Include(c=>c.User)
                  .FirstOrDefaultAsync(c => c.Id == updateCharacter.Id);
               if(character is null || character.User!.Id != GetUserId())
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

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
           var response =new ServiceResponse<GetCharacterDto>();
            try
            {
                var characet=await _context.Characters
                    .Include(c=>c.Weapon)
                    .Include(c=>c.Skills)
                    .FirstOrDefaultAsync(c=>c.Id==newCharacterSkill.CharaterId &&
                    c.User!.Id ==GetUserId());
                if (characet is null)
                {
                    response.Success = false;
                    response.Message = "Charater not found.";
                    return response;
                }
                var skill = await _context.Skills
                    .FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if (skill is null)
                {
                    response.Success = false;
                    response.Message = "Skill not found.";
                    return response;
                }
                characet.Skills!.Add(skill);
                await _context.SaveChangesAsync();
                response.Data = mapper.Map<GetCharacterDto>(characet);
            }
            catch (Exception ex)
            {
                response.Success=false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
