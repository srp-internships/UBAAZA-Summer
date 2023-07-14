using dotnet_rpg.Dtos.Fight;

namespace dotnet_rpg.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        Task<ServiceResponse<AttackResultDto>> Fight(SkillAttackDto request)
        Task<ServiceResponse<FightRequestDto>> Fight(FightRequestDto request)

    }
}
