using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace GameTetrisRace
{
    public partial class FormView : Form
    {
        #region Объявление переменных

        private int _row, _col;                   //Размерность игровой сетки
        private int _startX, _startY;             //Стартовая позиция
        private int _elementSize;                 //Размер клетки сетки
        private int _myCarPosition;               //Позиция машины игрока
        private int[,] _gameMatrix;               //Массив
        private int _carX, _carY, _carX2, _carY2; //Положение машин врагов
        private Random _random, _random2;         //Рандом
        private int score, speed;                 //Счётчики скорости и очков
        public int diff;                          //Сложность игры
        public int proverka;

        //Переменные для передачи цвета
        public System.Drawing.SolidBrush clrSetka = new System.Drawing.SolidBrush(Color.Brown);
        public System.Drawing.SolidBrush clrPlayer = new System.Drawing.SolidBrush(Color.DeepSkyBlue);
        public System.Drawing.SolidBrush clrVrag = new System.Drawing.SolidBrush(Color.DarkCyan);

        //Переменные для обращения к формам
        Help help = new Help();
        Settings st = new Settings();
        Authors authors = new Authors();

        #endregion

        public FormView()
        {
            InitializeComponent();
            InitializeGame();
        }

        //Значения переменных
        private void InitializeGame()
        {
            _row = 20;              //Кол-во клеток по высоте
            _col = 9;               //Кол-во клеток по ширине
            _startX = 20;           //Коорд-ты отрисовки игровой сетки
            _startY = 50;           //Коорд-ты отрисовки игровой сетки
            _elementSize = 20;      //Размер клетки игрового поля
            _carX = _carY = 0;      //Положение машины 1го врага
            _carX2 = _carY2 = 0;    //Положение машины 2го врага
            _gameMatrix = new int[_row, _col];
            ResetGameBoard();
            _random = new Random();
            _random2 = new Random();
            _myCarPosition = 3; //Стартовая позиция авто игрока (0 or 3 or 6)
            DrawACar(16, _myCarPosition, 2);
            score = 0;
            speed = 1;
            diff = 1;
            proverka = 0;
        }

        //Объявление поле формы областью для отрисовки
        private void FormView_Paint(object sender, PaintEventArgs e)
        {
            DrawGameBoard(e.Graphics);
        }

        //Занесение отрисовок в массивы
        private void DrawGameBoard(Graphics g) 
        {
            for (int i = 0; i < _row; i++)
            {
                for (int j = 0; j < _col; j++)
                {
                    //Отрисовка игровой сетки
                    g.DrawRectangle(new Pen(clrSetka), _startX + j * _elementSize, _startY + i * _elementSize, _elementSize, _elementSize);
                    
                    //Занесение в массив 1-го противника
                    if(_gameMatrix[i,j] == 1)
                    {
                        g.FillRectangle(clrVrag, _startX + j * _elementSize, _startY + i * _elementSize, _elementSize, _elementSize);
                    }

                    //Занесение в массив машину игрока
                    if (_gameMatrix[i, j] == 2)
                    {
                        g.FillRectangle(clrPlayer, _startX + j * _elementSize, _startY + i * _elementSize, _elementSize, _elementSize);
                    }
                }
            }
        }

        //Обновление массивов
        private void ResetGameBoard() 
        {
        for (int i = 0; i < _row; i++)
            {
                for (int j = 0; j < _col; j++)
                {
                    _gameMatrix[i,j] = 0;
                }
            }
        }

        //Отрисовка машины
        private void DrawACar(int x, int y, int value)
        {
            //Отрисовка (закрашивание клеток) машин
            DrawAPoint(x, y+1, value);
            DrawAPoint(x+1, y+1, value);
            DrawAPoint(x+2, y+1, value);
            DrawAPoint(x+3, y+1, value);
            DrawAPoint(x+1, y, value);
            DrawAPoint(x+1, y+2, value);
            DrawAPoint(x+3, y, value);
            DrawAPoint(x+3, y+2, value);
        }

        //Отрисовка
        private void DrawAPoint(int x, int y, int value)
        {
            if(x < _row && x >= 0 && y < _col && y >= 0)
            {
                _gameMatrix[x,y] = value;
            }
        }

        //Настройка процесса игры
        private void tmrRacing_Tick(object sender, EventArgs e)
        {
            score++;
            label2.Text = "Очки: " + score;
            label4.Text = "Скорость: " + speed;

            if(st.diff == 1)
            {
                diff = 1;
                label3.Text = "Сложность: Легко";
            }

            if (st.diff == 2)
            {
                diff = 2;
                label3.Text = "Сложность: Нормально";
            }

            if (st.diff == 3)
            {
                diff = 3;
                label3.Text = "Сложность: Сложно";
            }

            //Настройка скорости
            #region 1 уровень сложности

            if (diff == 1)
            {
                if (score >= 50)
                {
                    speed = 2;
                    tmrRacing.Interval = 70;
                    if (score >= 100)
                    {
                        speed = 3;
                        tmrRacing.Interval = 65;
                        if (score >= 150)
                        {
                            speed = 4;
                            tmrRacing.Interval = 60;
                            if (score >= 200)
                            {
                                speed = 5;
                                tmrRacing.Interval = 55;
                                if (score >= 300)
                                {
                                    speed = 6;
                                    tmrRacing.Interval = 50;
                                    if (score >= 450)
                                    {
                                        speed = 7;
                                        tmrRacing.Interval = 45;
                                        if (score >= 700)
                                        {
                                            speed = 8;
                                            tmrRacing.Interval = 40;
                                            if (score >= 900)
                                            {
                                                speed = 9;
                                                tmrRacing.Interval = 35;
                                                if (score >= 1300)
                                                {
                                                    speed = 10;
                                                    tmrRacing.Interval = 30;
                                                    if (score >= 1700)
                                                    {
                                                        speed = 11;
                                                        tmrRacing.Interval = 25;
                                                        if (score >= 2500)
                                                        {
                                                            speed = 12;
                                                            tmrRacing.Interval = 20;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            #region 2 уровень сложности

            if (diff == 2)
            {
                if (score >= 50)
                {
                    speed = 2;
                    tmrRacing.Interval = 70;
                    if (score >= 100)
                    {
                        speed = 3;
                        tmrRacing.Interval = 65;
                        if (score >= 150)
                        {
                            speed = 4;
                            tmrRacing.Interval = 60;
                            if (score >= 300)
                            {
                                speed = 5;
                                tmrRacing.Interval = 55;
                                if (score >= 450)
                                {
                                    speed = 6;
                                    tmrRacing.Interval = 50;
                                    if (score >= 700)
                                    {
                                        speed = 7;
                                        tmrRacing.Interval = 45;
                                        if (score >= 950)
                                        {
                                            speed = 8;
                                            tmrRacing.Interval = 40;
                                            if (score >= 1200)
                                            {
                                                speed = 9;
                                                tmrRacing.Interval = 35;
                                                if (score >= 1600)
                                                {
                                                    speed = 10;
                                                    tmrRacing.Interval = 30;
                                                    if (score >= 2000)
                                                    {
                                                        speed = 11;
                                                        tmrRacing.Interval = 25;
                                                        if (score >= 2500)
                                                        {
                                                            speed = 12;
                                                            tmrRacing.Interval = 20;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            #region 3 уровень сложности

            if (diff == 3)
            {
                if (score >= 50)
                {
                    speed = 2;
                    tmrRacing.Interval = 70;
                    if (score >= 100)
                    {
                        speed = 3;
                        tmrRacing.Interval = 65;
                        if (score >= 150)
                        {
                            speed = 4;
                            tmrRacing.Interval = 60;
                            if (score >= 200)
                            {
                                speed = 5;
                                tmrRacing.Interval = 55;
                                if (score >= 275)
                                {
                                    speed = 6;
                                    tmrRacing.Interval = 50;
                                    if (score >= 375)
                                    {
                                        speed = 7;
                                        tmrRacing.Interval = 45;
                                        if (score >= 500)
                                        {
                                            speed = 8;
                                            tmrRacing.Interval = 40;
                                            if (score >= 650)
                                            {
                                                speed = 9;
                                                tmrRacing.Interval = 35;
                                                if (score >= 850)
                                                {
                                                    speed = 10;
                                                    tmrRacing.Interval = 30;
                                                    if (score >= 1200)
                                                    {
                                                        speed = 11;
                                                        tmrRacing.Interval = 25;
                                                        if (score >= 1600)
                                                        {
                                                            speed = 12;
                                                            tmrRacing.Interval = 20;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            ResetGameBoard();
            DrawACar(16, _myCarPosition, 2);
            DrawACar(_carX, _carY, 1);
            DrawACar(_carX2, _carY2, 1);
            Invalidate();

            _carX++;
            if(_carX == _row)
            {
                _carX = 0;
                _carY = _random.Next(0, 3) * 3;
            }

            _carX2++;
            if (_carX2 == _row)
            {
                _carX2 = 0;
                _carY2 = _random2.Next() % 2 == 0 ? 0 : 3;
            }

            CheckGame();
        }
        
        //Управление
        private void FormView_KeyDown(object sender, KeyEventArgs e)
        {

            if(e.KeyCode == Keys.Escape)
            {
                Close();
            }

            if (e.KeyCode == Keys.Space)
            {
                proverka = 1;
                label9.Text = "";
                label8.Text = "";
                label5.Text = "";
                label7.Text = "";
                label6.Text = "Enter или F - пауза";
                tmrRacing.Enabled = true;
            }

            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.F))
            {
                
                if(proverka == 1)
                {
                    label6.Text = "";
                    label9.Text = "Пауза :)";
                    label7.Text = "Пробел - продолжить игру";
                    tmrRacing.Enabled = false;
                    proverka = 0;
                }
            }

            if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.A))
            {
                if(_myCarPosition >0 && proverka == 1)
                {
                    _myCarPosition = _myCarPosition - 3;
                }

                else
                {
                    _myCarPosition = _myCarPosition - 0;
                }
            }

            else if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.D))
            {
                if (_myCarPosition < 6 && proverka == 1)
                {
                    _myCarPosition = _myCarPosition + 3;
                }

                else
                {
                    _myCarPosition = _myCarPosition + 0;
                }
            }
        }

        //Проверка игры на столкновение
        private void CheckGame()
        {
            if (_carX + 3 > 16 && _carY == _myCarPosition)
            {
                tmrRacing.Enabled = false;
                _carX = _carY = 0;
                _carX2 = _carY2 = 0;
                _gameMatrix = new int[_row, _col];
                ResetGameBoard();
                _random = new Random();
                _random2 = new Random();
                _myCarPosition = 3; //Стартовая позиция авто игрока (0 or 3 or 6)
                DrawACar(16, _myCarPosition, 2);
                speed = 1;
                score = 0;
                label7.Text = "";
                label6.Text = "";
                label5.Text = "Чтобы начать игру\nнажми пробел :)";
                label8.Text = "Проиграл :с";
                tmrRacing.Interval = 75;
                proverka = 0;
            }

            if (_carX2 + 3 > 16 && _carY2 == _myCarPosition)
            {
                tmrRacing.Enabled = false;
                _carX = _carY = 0;
                _carX2 = _carY2 = 0;
                _gameMatrix = new int[_row, _col];
                ResetGameBoard();
                _random = new Random();
                _random2 = new Random();
                _myCarPosition = 3; //Стартовая позиция авто игрока (0 or 3 or 6)
                DrawACar(16, _myCarPosition, 2);
                speed = 1;
                score = 0;
                label7.Text = "";
                label6.Text = "";
                label5.Text = "Чтобы начать игру\nнажми пробел :)";
                label8.Text = "Проиграл :с";
                tmrRacing.Interval = 75;
                proverka = 0;
            }
        }

        //Запуск формы
        private void FormView_Load(object sender, EventArgs e)
        {
            tmrRacing.Enabled = false;
            label2.Text = "Очки: " + score;
            label4.Text = "Скорость: " + speed;

            if (diff == 1)
            {
                label3.Text = "Сложность: Легко";
            }

            if (diff == 2)
            {
                label3.Text = "Сложность: Нормально";
            }

            if (diff == 3)
            {
                label3.Text = "Сложность: Сложно";
            }
        }

        //Диалоговое окно "Подтверждение выхода из игры"
        private void FormView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(tmrRacing.Enabled == true)
            {
                label9.Text = "Пауза :)";
                label7.Text = "Пробел - продолжить игру";
                label6.Text = "";
                label8.Text = "";
                tmrRacing.Enabled = false;
            }

            if (MessageBox.Show("Вы уверены, что хотите выйти?", "", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                e.Cancel = true;
            }

            else
            {
                e.Cancel = false;
            }

            label6.Text = "";
        }

        //Диалоговое окно "Помощь"
        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(tmrRacing.Enabled == true)
            {
                tmrRacing.Enabled = false;
                label9.Text = "Пауза :)";
                label6.Text = "";
                label7.Text = "Пробел - продолжить игру";
            }

            else
            {
                label6.Text = "";
            }
            
            help.ShowDialog();
        }

        //Диалоговое окно "Настройки"
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tmrRacing.Enabled == true)
            {
                tmrRacing.Enabled = false;
                label9.Text = "Пауза :)";
                label6.Text = "";
                label7.Text = "Пробел - продолжить игру";
            }

            else
            {
                label6.Text = "";
            }

            st.ShowDialog();

            if (st.diff == 1)
            {
                diff = 1;
                label3.Text = "Сложность: Легко";
            }

            if (st.diff == 2)
            {
                diff = 2;
                label3.Text = "Сложность: Нормально";
            }

            if (st.diff == 3)
            {
                diff = 3;
                label3.Text = "Сложность: Сложно";
            }

            clrPlayer = st.clrPlayer;
            clrVrag = st.clrVrag;
            clrSetka = st.clrSetka;

            Invalidate();
        }

        //Диалоговое окно "Об авторе"
        private void обАвтореToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tmrRacing.Enabled == true)
            {
                tmrRacing.Enabled = false;
                label9.Text = "Пауза :)";
                label6.Text = "";
                label7.Text = "Пробел - продолжить игру";
            }

            else
            {
                label6.Text = "";
            }

            authors.ShowDialog();
        }
    }
}