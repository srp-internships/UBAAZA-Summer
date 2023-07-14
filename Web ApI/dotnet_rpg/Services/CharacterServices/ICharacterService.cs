

namespace dotnet_rpg.Services.CharacterServices
{
    public interface ICharacterService
    {
        Task <ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
        Task <ServiceResponse<GetCharacterDto>> GetCharacterById(int Id);
        Task <ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
        Task <ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto UpdatedCharacter);

        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharater(int id);
        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);

    }
}
