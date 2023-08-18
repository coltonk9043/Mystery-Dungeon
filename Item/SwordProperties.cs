namespace DungeonGame.Item
{
    public class SwordProperties
    {
        private int dmg;
        private float critrate;
        private float speed;
        private float burning;
        private float lifesteal;
        private float armorpen;
        private float poison;
        private float slowdown;

        public SwordProperties(int dmg, float critrate, float speed)
        {
            this.dmg = dmg;
            this.critrate = critrate;
            this.speed = speed;
        }

        public int getDamage() => this.dmg;

        public SwordProperties setDamage(int dmg)
        {
            this.dmg = dmg;
            return this;
        }

        public float getCritRate() => this.critrate;

        public SwordProperties setCritRate(float critrate)
        {
            this.critrate = critrate;
            return this;
        }
    }
}
