﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using MouseHeatmap.Collector;
using System;

namespace MouseHeatmap.Collector.Migrations
{
    [DbContext(typeof(MouseHeatmapDbContext))]
    [Migration("20180305153133_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("MouseHeatmap.Collector.ScreenUnit", b =>
                {
                    b.Property<int>("ScreenUnitId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MouseEnteredCount");

                    b.Property<int>("X");

                    b.Property<int>("Y");

                    b.HasKey("ScreenUnitId");

                    b.ToTable("ScreenUnits");
                });
#pragma warning restore 612, 618
        }
    }
}
