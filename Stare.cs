using System;
using System.Collections.Generic;
using System.Text;

namespace puzzle
{
    class Stare
    {
        public enum Actiuni
        {
            INIT,
            L,
            R,
            U,
            D
        }
        int[,] _elemente;
        public static readonly int _valoareElementZero = 0;
        int[] _elementZero;
        public Actiuni _actiune;
        Stare _stareAnterioara;
        int _cost;

        public Stare(int[,] elemente, Stare stareAnterioara, Actiuni actiune)
        {
            _elemente = (int[,])elemente.Clone();
            _stareAnterioara = stareAnterioara;
            _actiune = actiune;
            _elementZero = (int[])GetElementZero();
        }

        private Array GetElementZero()
        {
            for (int i = 0; i < Randuri; i++)
            {
                for (int j = 0; j < Coloane; j++)
                {
                    if (_elemente[i, j] == _valoareElementZero)
                    {
                        return new int[] { i, j };
                    }
                }
            }

            throw new Exception(String.Format("Lipseste elementul cu valoare {0}", _valoareElementZero));
        }

        public bool AreSolutie()
        {
            int nrInversiuni = 0;
            for (int i = 0; i < Randuri; i++)
            {
                for (int j = 0; j < Coloane; j++)
                {
                    for (int p = i; p < Randuri; p++)
                    {
                        for (int t = (p == i ? j + 1 : 0); t < Coloane; t++)
                        {
                            if (_elemente[i, j] > _elemente[p, t] && _elemente[i, j] != _valoareElementZero && _elemente[p, t] != _valoareElementZero)
                            {
                                nrInversiuni++;
                            }
                        }
                    }
                }
            }
            if (Coloane % 2 == 1 && nrInversiuni % 2 == 0 || (Coloane % 2 == 0 && (((Coloane - _elementZero[1]) % 2 == 1) == (nrInversiuni % 2 == 0))))
            {
                return true;
            }
            return false;
        }

        public bool EsteSolutie()
        {
            int prevVal = 0;

            for (int i = 0; i < Randuri; i++)
            {
                for (int j = 0; j < Coloane; j++)
                {
                    int val = _elemente[i, j];

                    if (val > prevVal
                        || (val == _valoareElementZero && i == Randuri - 1 && j == Coloane - 1))
                    {
                        prevVal = val;
                    }
                    else
                    {
                        return false;
                    }

                }
            }

            return true;
        }

        private Stare GetStareNoua(int offsetRand, int offsetColoana, Actiuni actiune)
        {
            var elementeNoi = (int[,])_elemente.Clone();

            int swap = elementeNoi[_elementZero[0] + offsetRand, _elementZero[1] + offsetColoana];
            elementeNoi[_elementZero[0] + offsetRand, _elementZero[1] + offsetColoana] = elementeNoi[_elementZero[0], _elementZero[1]];
            elementeNoi[_elementZero[0], _elementZero[1]] = swap;

            return new Stare(elementeNoi, this, actiune);
        }

        public List<Stare> GetStariUrmatoare()
        {
            var stari = new List<Stare>();

            // Jos
            if (_elementZero[0] < Randuri - 1)
            {
                var stareNoua = GetStareNoua(1, 0, Actiuni.D);
                stari.Add(stareNoua);
            }

            // Dreapta
            if (_elementZero[1] < Coloane - 1)
            {
                var stareNoua = GetStareNoua(0, 1, Actiuni.R);
                stari.Add(stareNoua);
            }

            // Stanga
            if (_elementZero[1] > 0)
            {
                var stareNoua = GetStareNoua(0, -1, Actiuni.L);
                stari.Add(stareNoua);
            }

            // Sus
            if (_elementZero[0] > 0)
            {
                var stareNoua = GetStareNoua(-1, 0, Actiuni.U);
                stari.Add(stareNoua);
            }

            return stari;
        }

        public string Print()
        {
            var sb = new StringBuilder();
            foreach(int val in _elemente)
            {
                sb.Append(Convert.ToString(val));
                sb.Append(" ");
            }
            sb.Append("\n");
            sb.Append(_actiune);
            sb.Append("\n");
            return sb.ToString();
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (int val in _elemente)
            {
                sb.Append(Convert.ToString(val));
                sb.Append('_');
            }

            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public int Randuri
        {
            get { return _elemente.GetLength(0); }
        }

        public int Coloane
        {
            get { return _elemente.GetLength(1); }
        }

        public Stare StareAnterioara
        {
            get { return _stareAnterioara; }
        }

        public int Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
    }
}
