using Dominio.Enum;

namespace Dominio.Entidades
{
    public class Assinatura : Base
    {
        public TipoAssinatura TipoAssinatura { get; private set; }
        public double Preco { get; private set; }
        public int TipoPlano { get; private set; }
        public bool PodeComentar { get => true; }

        public Assinatura()
        {
                
        }

        public Assinatura(TipoAssinatura tipoAssinatura) : base()
        {
            AdicionaAssinatura(tipoAssinatura);
        }

        private void AdicionaAssinatura(TipoAssinatura tipoAssinatura)
        {
            TipoAssinatura = tipoAssinatura;
            CalculaPreco();
            CalculaPeriodicidade();
        }

        private void CalculaPreco()
        {
            switch (TipoAssinatura)
            {
                case TipoAssinatura.BASICO: Preco = 0; break;
                case TipoAssinatura.PAGO: Preco = 50; break;
                default: throw new ArgumentException("Não foi possivel encontrar Preço para o TipoAssinatura ", nameof(Preco));
            }
        }

        private void CalculaPeriodicidade()
        {
            switch (TipoAssinatura) 
            {
                case TipoAssinatura.BASICO: TipoPlano = 360; break;
                case TipoAssinatura.PAGO:   TipoPlano = 30; break;
                default: throw new ArgumentException("Não foi possivel encontrar Preço para o TipoPlano ", nameof(TipoPlano));
            }
        }
    }
}
