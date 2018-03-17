using System;

namespace MouseHeatmap.Collector
{
    internal class LinearSystem
    {
        private double _a1;
        private double _b1;
        private double _c1;
        private double _a2;
        private double _b2;
        private double _c2;




        public LinearSystem(double a1, double b1, double c1, double a2, double b2, double c2)
        {
            this._a1 = a1;
            this._b1 = b1;
            this._c1 = c1;
            this._a2 = a2;
            this._b2 = b2;
            this._c2 = c2;
        }

        internal SystemSolution Solve()
        {
            if(_a1 == 0 && _b1 == 0 && _a2 == 0 && _b2 == 0)
            {
                throw new Exception("All coefficients are 0");
            }

            var w = _a1 * _b2 - _b1 * _a2;
            var wx = _c1 * _b2 - _b1 * _c2;
            var wy = _a1 * _c2 - _c1 * _a2;

            if(w==0 && wx == 0 && wy == 0)
            {               
                return new SystemSolution {
                    IsIndeterminate = true
            };
            }

            if (w == 0 && wx != 0 && wy != 0)
            {
                return new SystemSolution
                {
                    IsContradictory = true
                };
            }


            return new SystemSolution
            {
                X = wx / w,
            Y = wy / w
        };
        
        }
    }
}