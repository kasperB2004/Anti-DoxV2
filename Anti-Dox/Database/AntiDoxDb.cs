using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;

namespace Anti_Dox.Database
{
    public partial class AntiDoxDb : DbContext
    {

        public virtual DbSet<PrefixList> PrefixList { get; set; }
        public virtual DbSet<BannedGuild> BannedGuild { get; set; }
        public virtual DbSet<MuteRoleId> MuteRoleId { get; set; }
        public virtual DbSet<BannedWords> BannedWords { get; set;}
        public virtual DbSet<WebsiteList> WebsiteList { get; set; }
        public virtual DbSet<Logs> Logs { get; set; }
        public virtual DbSet<RemovePunishment> RemovePunishment { get; set; }
        public virtual DbSet<IpSettings> IpSettings { get; set; }
        public virtual DbSet<WebsiteSettings> WebsiteSettings { get; set; }
        public virtual DbSet<SocialMediaSettings> SocialMediaSettings { get; set; }
        public virtual DbSet<PostCodeSettings> PostCodeSettings { get; set; }
        public virtual DbSet<BadWordSettings> BadWordSettings { get; set; }
        public virtual DbSet<LogSettings> LogSettings { get; set; }
        public virtual DbSet<IgnoreRoleSettings> IgnoreRoleSettings { get; set; }
        public virtual DbSet<AnnoucementSettings> AnnoucementSettings { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<EmailSettings> EmailSettings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Port=3306;Uid=root;Pwd=Death_day1?1;database=antidox;");
        }
    }
}