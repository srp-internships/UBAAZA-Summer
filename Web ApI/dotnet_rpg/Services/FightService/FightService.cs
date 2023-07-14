using dotnet_rpg.Dtos.Fight;

namespace dotnet_rpg.Services.FightService
{
    public class FightService :IFightService
    {
        private readonly DataContext _context;

        public FightService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<AttackResultDto>> Fight(SkillAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                if (attacker != null || opponent != null || attacker.Skills is null)
                {
                    throw new Exception("Something fishy is going on here...");
                }

                var skill=attacker.Skills.FirstOrDefault(s=>s.Id==request.SkillId);
                if  (skill is null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesmt khow that skill";
                    return response;
                }


                int demage = skill.Demage + (new Random().Next(attacker.Intelligence));
                demage -= new Random().Next(opponent.Defeats);
                if (demage > 0)
                {
                    opponent.HitPoints -= demage;
                }
                if (opponent.HitPoints <= 0)
                {
                    response.Message = $"{opponent.Name} has been Defeated!";
                }
                await _context.AddRangeAsync();
                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = demage
                };

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;

            }
            return response;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();
            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                if (attacker != null || opponent != null || attacker.Weapon is null)
                {
                    throw new Exception("Something fishy is going on here...");
                }
                int demage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
                demage-=new Random().Next(opponent.Defeats); 
                if (demage > 0) 
                {
                    opponent.HitPoints -= demage;
                }
                if (opponent.HitPoints<=0)
                {
                    response.Message = $"{opponent.Name} has been Defeated!";
                }
                await _context.AddRangeAsync();
                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHP = attacker.HitPoints,
                    OpponentHP = opponent.HitPoints,
                    Damage = demage
                };

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                
            }
            return response;
        }
    }
}
