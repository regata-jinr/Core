/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2021, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 *                                                                         *
 * The REGATA Experiment team license this file to you under the           *
 * GNU GENERAL PUBLIC LICENSE                                              *
 *                                                                         *
 ***************************************************************************/

using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.Core.DataBase.Models
{
    public class Position : IEquatable<Position>
    {
        public string Name         { get; set; }
        public int    SerialNumber { get; set; }
        public string Detector     { get; set; }
        public int    X            { get; set; }
        public int    Y            { get; set; }
        public int?   C            { get; set; }

        [NotMapped]
        public int Delta { get; set; } = 5;

        public override string ToString() => $"X = {X}, Y = {Y}, C = {C}";

        public bool Equals(Position target)
        {
            if (target == null)
                return false;

            var iseq = (Math.Abs(target.X - this.X) <= Delta) && (Math.Abs(target.Y - this.Y) <= Delta);

            if (target.C.HasValue && this.C.HasValue)
                iseq = iseq && (Math.Abs(target.C.Value - this.C.Value) <= Delta);

            return iseq;
        }

        public override bool Equals(object obj) => this.Equals(obj as Position);

        public static bool operator ==(Position lhs, Position rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                {
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Position lhs, Position rhs) => !(lhs == rhs);


        public override int GetHashCode()
        {
            if (C.HasValue)
                return HashCode.Combine(X, Y, C.Value, SerialNumber, Name);
            
            return HashCode.Combine(X, Y, SerialNumber, Name);
        }

    } // public class Position : IEquatable<Position>
}     // namespace Regata.Core.DataBase.Models
