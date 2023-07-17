using AutoMapper.Execution;
using dotnet_rpg.Dtos.Fight;

namespace dotnet_rpg.Services.FightService
{
    public class FightService :IFightService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FightService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
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

                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);
                if (skill is null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesmt khow that skill";
                    return response;
                }

                int demage = DoSkillAttack(attacker, opponent, skill);
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

        private static int DoSkillAttack(Character attacker, Character opponent, Skill skill)
        {
            int demage = skill.Demage + (new Random().Next(attacker.Intelligence));
            demage -= new Random().Next(opponent.Defeats);
            if (demage > 0)
            {
                opponent.HitPoints -= demage;
            }

            return demage;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            var response = new ServiceResponse<FightResultDto>
            {

                Data =new FightResultDto()
            };
            try
            {
                var characters= await _context.Characters
                    .Include(c=>c.Weapon)
                    .Include(c=>c.Skills)
                    .Where(c=>request.CharacterIds.Contains(c.Id))
                    .ToListAsync();
                bool defeated = false;
                while (!defeated)
                {
                    foreach(var attacker in characters)
                    {
                        var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count)];

                        int damage = 0;
                        string attackUsed=string.Empty;

                        bool useWeapon = new Random().Next(2) == 0;
                        if (useWeapon && attacker.Weapon is not null)
                        {
                            attackUsed = attacker.Weapon.name;
                            damage = DoWeaponAttack(attacker, opponent);
                        }
                        else if (!useWeapon && attacker.Skills is not null)
                        {
                            var skill = attacker.Skills[new Random().Next(attacker.Skills.Count)];
                            attackUsed = skill.Name;
                            damage = DoSkillAttack(attacker, opponent, skill);
                        }
                        else
                        {
                            response.Data.Log
                                .Add($"{attacker.Name} wasn't able to attack!");
                            continue;
                        }
                        response.Data.Log
                            .Add($"{attacker.Name} attacks {opponent.Name} using {attackUsed} with{(damage >= 0 ? damage : 0)} damage ");
                   
                        if(opponent.HitPoints<=0)
                        {
                            defeated = true;
                            attacker.Victoties++;
                            opponent.Defeats++;
                            response.Data.Log.Add($"{opponent.Name} has been defeated");
                            response.Data.Log.Add($"{attacker.Name} wins with {attacker.HitPoints} HP left !");
                        }
                    }
                }
                characters.ForEach(c =>
                {
                    c.fights++;
                    c.HitPoints = 100;
                });
                await _context.SaveChangesAsync();

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
                int demage = DoWeaponAttack(attacker, opponent);
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

        private static int DoWeaponAttack(Character attacker, Character opponent)
        {
            if (attacker.Weapon is null)
                throw new Exception("Attecker has no weapon !");
            int demage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
            demage -= new Random().Next(opponent.Defeats);
            if (demage > 0)
            {
                opponent.HitPoints -= demage;
            }

            return demage;
        }

        public async Task<ServiceResponse<List<HighscoreDto>>> GetHighscore()
        {
            var  characters=await _context.Characters
                .Where(c=>c.fights>0)
                .OrderByDescending(c=>c.Victoties)
                .ThenBy(c=>c.Defense)
                .ToArrayAsync();
            var response=new ServiceResponse<List<HighscoreDto>>()
            {
                Data=characters.Select(c=>_mapper.Map<HighscoreDto>(c)).ToList()
            };
            return response;
        }
    }
}
