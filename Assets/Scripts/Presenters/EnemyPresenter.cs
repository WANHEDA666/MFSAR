using Views;

namespace Presenters
{
    public class EnemyPresenter
    {
        private readonly EnemyView enemyView;

        public EnemyPresenter(EnemyView enemyView)
        {
            this.enemyView = enemyView;
        }
    }
}