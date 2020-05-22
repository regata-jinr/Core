/***************************************************************************
 *                                                                         *
 *                                                                         *
 * Copyright(c) 2020, REGATA Experiment at FLNP|JINR                       *
 * Author: [Boris Rumyantsev](mailto:bdrum@jinr.ru)                        *
 * All rights reserved                                                     *
 *                                                                         *
 *                                                                         *
 ***************************************************************************/

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Regata.UITemplates
{



    [Table("UILabels")]
    public class UILabels
    {
        public string FormName { get; set; }
        public string ComponentName { get; set; }
        public string RusText { get; set; }
        public string EngText { get; set; }
    }

    public class LabelsContext : DbContext
    {
        public DbSet<UILabels> Labels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Settings.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UILabels>()
                   .HasKey(u => new { u.FormName, u.ComponentName });
        }
    }

    public class Labels
    {

        public IReadOnlyList<UILabels> FormLabels;

        public Labels(string formName)
        {
            using (var lc = new LabelsContext())
            {
                FormLabels = lc.Labels.Where(l => l.FormName == formName).ToArray();
            }
        }

        public string GetLabel(string compt)
        {
            if (!FormLabels.Select(l => l.ComponentName).Contains(compt)) return "";
            if (Settings.CurrentLanguage == Languages.Russian)
                return FormLabels.Where(f => f.ComponentName == compt).Select(f => f.RusText).First();
            else
                return FormLabels.Where(f => f.ComponentName == compt).Select(f => f.EngText).First();
        }

      
    } // public static class Labels
} // namespace Regata.UITemplates
