using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ChallengeHeroMonsterClassesPart2
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Character hero = new Character();
            hero.Name = "Hero";
            hero.Health = 100;
            hero.DamageMaximum = 15;
            hero.AttackBonus = false;

            Character monster = new Character();
            monster.Name = "Monster";
            monster.Health = 150;
            monster.DamageMaximum = 20;
            monster.AttackBonus = true;

            Dice d20 = new Dice();
            d20.Sides = 20;

            Random random = new Random();
            int bonusCheck = random.Next();

            if (hero.AttackBonus)
            {
                resultLabel.Text += String.Format("<p>{0} was sneaky and attacked before {1} was ready!<p>", hero.Name, monster.Name);
                monster.Defend(hero.Attack(d20.Roll()));
            }

            if (monster.AttackBonus)
            {
                resultLabel.Text += String.Format("<p>{0} was sneaky and attacked before {1} was ready!<p>", monster.Name, hero.Name);
                hero.Defend(monster.Attack(d20.Roll()));
            }

            resultLabel.Text += "Let the battle begin!<br />";

            int round = 0;

            while (hero.Health > 0 && monster.Health > 0)
            {
                
                round++;
                printStats(hero);
                printStats(monster);

                resultLabel.Text += "<p>Round " + round.ToString() + ": <p>";

                int damage = hero.Attack(d20.Roll());
                monster.Defend(damage);
                resultLabel.Text += String.Format("<p>{0} attacks, dealing {1} points of damage to {2}<p>",
                    hero.Name, damage, monster.Name);

                damage = monster.Attack(d20.Roll());
                hero.Defend(damage);
                resultLabel.Text += String.Format("<p>{0} attacks, dealing {1} points of damage to {2}<p>",
                    monster.Name, damage, hero.Name);
            }

            displayResult(hero, monster);

            /*if (hero.Health <= 0 && monster.Health <= 0)
                {
                    resultLabel.Text += String.Format("<p>After a long and protracted battle, {0} slew {1} but then, alas, {0} "
                        + " succumbed to her own injuries.<p>", hero.Name, monster.Name);
                    resultLabel.Text += "<P> Final Stats: <p>";

                    printStats(hero);
                    printStats(monster);
                }
                else if (hero.Health <= 0)
                {
                    resultLabel.Text += String.Format("<p>Pushed beyond the limits of her endurance, {0} stares into the abyss of {1} and goes utterly mad.", hero.Name, monster.Name);
                    resultLabel.Text += "<P> Final Stats: <p>";

                    printStats(hero);
                    printStats(monster);
                }
                else
                {
                    resultLabel.Text += String.Format("<p>{0} stands triumphant, having defeated {1} unequivacably.", hero.Name, monster.Name);
                    resultLabel.Text += "<P> Final Stats: <p>";

                    printStats(hero);
                    printStats(monster);
                } */
        }

        private void displayResult(Character opponent1, Character opponent2)
        {
            if (opponent1.Health <= 0 && opponent2.Health <= 0)
            {
                resultLabel.Text += String.Format("<p>After a long and protracted battle, {0} slew {1} but then, alas, {0} "
                    + " succumbed to her own injuries.<p>", opponent1.Name, opponent2.Name);
            }
            else if (opponent1.Health <= 0)
            {
                resultLabel.Text += String.Format("<p>Pushed beyond the limits of her endurance, {0} stares into the abyss of {1} "
                    + "and goes utterly mad.", opponent1.Name, opponent2.Name);
            }
            else
            {
                resultLabel.Text += String.Format("<p>{0} stands triumphant, having defeated {1} unequivacably.", opponent1.Name, opponent2.Name);
            }
            resultLabel.Text += "<P> Final Stats: <p>";
            printStats(opponent1);
            printStats(opponent2);
        }

        private void printStats(Character character)
        {
            resultLabel.Text += String.Format("<p>Name: {0} - Health: {1} - DamageMaximum: {2} - AttackBonus: {3}</p>",
                character.Name,
                character.Health,
                character.DamageMaximum.ToString(),
                character.AttackBonus.ToString());
        }

    }

    class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int DamageMaximum { get; set; }
        public bool AttackBonus { get; set; }

        public int Attack(int diceRoll)
        {
            int damage = diceRoll;
            return damage;
        }

        public void Defend(int damage)
        {
            this.Health -= damage;
        }
    }

    class Dice
    {
        public int Sides { get; set; }

        Random random = new Random();

        public int Roll()
        {
            int rollOutcome = random.Next(this.Sides);
            return rollOutcome;
        }
    }
}