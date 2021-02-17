using System;
using System.Collections;
using System.Collections.Generic;

namespace puzzle
{
    class RezolvarePuzzle
    {
        Stare _stareInitiala;
        Stare _stareFinala = null;
        Queue<Stare> _frontiera = new Queue<Stare>();
        int _stariExplorate = 0;
        Dictionary<string, Stare> _stariGenerate = new Dictionary<string, Stare>();

        public RezolvarePuzzle(Stare stareInitiala)
        {
            _stareInitiala = stareInitiala;
        }

        public bool Rezolva()
        {
            // Nu are solutie
            if (!_stareInitiala.AreSolutie())
            {
                return false;
            }

            // Este deja rezolvat
            if (_stareFinala != null)
            {
                return true;
            }

            _frontiera.Enqueue(_stareInitiala);
            _stariGenerate.Add(_stareInitiala.ToString(), _stareInitiala);

            do
            {
                var stareCurenta = _frontiera.Dequeue();
                _stariExplorate++;

                if (stareCurenta.EsteSolutie())
                {
                    _stareFinala = stareCurenta;
                    break;
                }

                foreach (var stareUrmatoare in stareCurenta.GetStariUrmatoare())
                {
                    if (!_stariGenerate.ContainsKey(stareUrmatoare.ToString()))
                    {
                        // O adaugam pentru explorare, doar daca nu a mai fost explorata
                        _frontiera.Enqueue(stareUrmatoare);
                        _stariGenerate.Add(stareUrmatoare.ToString(), stareUrmatoare);
                    }
                }
            } while (_frontiera.Count > 0);

            return true;
        }

        public List<Stare> GetPasiRezolvare()
        {
            var lista = new List<Stare>();

            var stareCurenta = _stareFinala;

            while (stareCurenta != null)
            {
                lista.Add(stareCurenta);
                stareCurenta = stareCurenta.StareAnterioara;
            }

            lista.Reverse();

            return lista;
        }

        public Stare StareFinala
        {
            get { return _stareFinala; }
        }

        public int NumarStariGenerate
        {
            get { return _stariGenerate.Count; }
        }

        public int NumarStariExplorate
        {
            get { return _stariExplorate; }
        }
    }
}
