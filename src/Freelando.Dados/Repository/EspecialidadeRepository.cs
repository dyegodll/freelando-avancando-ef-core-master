using Freelando.Dados;
using Freelando.Dados.Repository;
using Freelando.Modelo;

public class EspecialidadeRepository : Repository<Especialidade>, IEspecialidadeRepository
{
    public EspecialidadeRepository(FreelandoContext context) : base(context)
    {
    }
}
