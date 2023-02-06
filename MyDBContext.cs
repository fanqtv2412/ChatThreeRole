using System.Text.RegularExpressions;
using ChatThreeRole.Models;
using Microsoft.EntityFrameworkCore;

public class MyDBContext : DbContext{
    public MyDBContext(DbContextOptions<MyDBContext> options) : base(options){}

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<AccountModel>(entity=>{
            entity.ToTable("tbl_account");
            entity.HasKey(e => e.Email).HasName("tbl_account_key");
        });
        modelBuilder.Entity<GroupModel>(entity=>{
            entity.ToTable("tbl_group");
            entity.HasKey(e => e.Id);
        });
        modelBuilder.Entity<MessageModel>(entity=>{
            entity.ToTable("tbl_message");
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Group)
            .WithMany(e => e.MessageModels)
            .OnDelete(DeleteBehavior.ClientSetNull);
        });
    }
    public DbSet<AccountModel> Account {get; set;}
    public DbSet<GroupModel> Group {get; set;}
    public DbSet<MessageModel> Messages {get; set;}

}