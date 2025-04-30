using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;

namespace WPF_Projekt
{
    public class BattleViewModel : INotifyPropertyChanged
    {
        public Character Player { get; set; }
        public Character Enemy { get; set; }

        private Ability _selectedAbility;
        public Ability SelectedAbility
        {
            get => _selectedAbility;
            set
            {
                _selectedAbility = value;
                OnPropertyChanged();
                ((RelayCommand)UseAbilityCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand UseAbilityCommand { get; }
        public ICommand AttackCommand { get; }

        private string _log;
        public string Log
        {
            get => _log;
            set { _log = value; OnPropertyChanged(); }
        }

        private string _playerDamageEffectPath;
        public string PlayerDamageEffectPath
        {
            get => _playerDamageEffectPath;
            set { _playerDamageEffectPath = value; OnPropertyChanged(); }
        }

        private Visibility _playerDamageEffectVisibility = Visibility.Collapsed;
        public Visibility PlayerDamageEffectVisibility
        {
            get => _playerDamageEffectVisibility;
            set { _playerDamageEffectVisibility = value; OnPropertyChanged(); }
        }

        private string _enemyDamageEffectPath;
        public string EnemyDamageEffectPath
        {
            get => _enemyDamageEffectPath;
            set { _enemyDamageEffectPath = value; OnPropertyChanged(); }
        }

        private Visibility _enemyDamageEffectVisibility = Visibility.Collapsed;
        public Visibility EnemyDamageEffectVisibility
        {
            get => _enemyDamageEffectVisibility;
            set { _enemyDamageEffectVisibility = value; OnPropertyChanged(); }
        }


        public BattleViewModel()
        {
            Player = new Character { Name = "Hero", HP = 100, MaxHP = 100, Attack = 20, ImagePath = "images/m4.gif" };
            Enemy = new Character { Name = "Monster", HP = 80, MaxHP = 80, Attack = 15, ImagePath = "images/m5.gif" };
            Player.Abilities.AddRange(new[]
            {
                new Ability { Name = "Slash", Damage = 15, Description = "A quick strike.", AnimationPath = "images/bolt.gif" },
                new Ability { Name = "Fireball", Damage = 30, Description = "A powerful flame attack.", AnimationPath = "images/fire.gif" }
            });

            UseAbilityCommand = new RelayCommand(async () => await UseAbility(), () => SelectedAbility != null && Enemy.IsAlive && Player.IsAlive);
            AttackCommand = new RelayCommand(async () => await Attack(), () => IsPlayerTurn && Enemy.IsAlive && Player.IsAlive);

            Log = "A harc elkezdődött!";
        }

        private async Task Attack()
        {
            if (!IsPlayerTurn || !Enemy.IsAlive || !Player.IsAlive)
                return;

            Enemy.TakeDamage(Player.Attack);
            EnemyDamageEffectPath = "images/lightning.gif";
            EnemyDamageEffectVisibility = Visibility.Visible;

            Log = $"{Player.Name} megtámadta {Enemy.Name}-t, és sebzett {Player.Attack} pontot.";
            OnPropertyChanged(nameof(Enemy));

            await Task.Delay(1100);
            EnemyDamageEffectPath = null;

            if (!Enemy.IsAlive)
            {
                Log += $"\n{Enemy.Name} legyőzve!";
                return;
            }

            CurrentTurn = Turn.Enemy;
            await Task.Delay(2000);

            EnemyAttack();
        }

        private void EnemyAttack()
        {
            Player.TakeDamage(Enemy.Attack);
            PlayerDamageEffectPath = "images/lightning.gif";
            PlayerDamageEffectVisibility = Visibility.Visible;

            Log += $"\n{Enemy.Name} támadott és sebzett {Enemy.Attack} pontot.";
            OnPropertyChanged(nameof(Player));

            Task.Delay(1100).ContinueWith(_ => PlayerDamageEffectPath = null);

            if (!Player.IsAlive)
            {
                Log += $"\n{Player.Name} legyőzve!";
                return;
            }

            CurrentTurn = Turn.Player;
        }

        private async Task UseAbility()
        {
            if (!IsPlayerTurn || SelectedAbility == null || !Enemy.IsAlive || !Player.IsAlive)
                return;

            // Sebzés alkalmazása
            Enemy.TakeDamage(SelectedAbility.Damage);

            // Animáció: csak az ellenfélre vonatkozik
            EnemyDamageEffectPath = SelectedAbility.AnimationPath;
            EnemyDamageEffectVisibility = Visibility.Visible;

            // Napló frissítése
            Log = $"{Player.Name} használta: {SelectedAbility.Name}, és sebzett {SelectedAbility.Damage} pontot.";
            OnPropertyChanged(nameof(Enemy));

            // Várunk, amíg az animáció lejátszódik
            await Task.Delay(1100);
            EnemyDamageEffectVisibility = Visibility.Collapsed;

            if (!Enemy.IsAlive)
            {
                Log += $"\n{Enemy.Name} legyőzve!";
                return;
            }

            // Ellenfél köre
            CurrentTurn = Turn.Enemy;
            await Task.Delay(2000);

            EnemyAttack();
        }


        private Turn _currentTurn = Turn.Player;
        public Turn CurrentTurn
        {
            get => _currentTurn;
            set
            {
                _currentTurn = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsPlayerTurn));
            }
        }

        public bool IsPlayerTurn => CurrentTurn == Turn.Player;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
