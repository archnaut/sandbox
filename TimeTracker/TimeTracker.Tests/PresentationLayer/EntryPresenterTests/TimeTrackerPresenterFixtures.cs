// ReSharper disable InconsistentNaming
using System;
using System.Windows.Forms;
using NUnit.Framework;
using Rhino.Mocks;
using TimeTracker.Domain;
using TimeTracker.DomainLayer;
using TimeTracker.PresentationLayer;
using StructureMap.AutoMocking;

namespace TimeTracker.Tests.PresentationLayer.EntryPresenterTests
{
    [TestFixture]
    public class TimeEntryPresenterFixture
    {
        [SetUp]
        public virtual void SetUp()
        {
            CreateSUT();
        }

        protected IRepository Repository{get; set;}
        
        protected ITaskEntryView View { get; set; }

        protected virtual void CreateSUT()
        {
        	Repository = MockRepository.GenerateStub<IRepository>();
            View = MockRepository.GenerateMock<ITaskEntryView>();

            new TaskEntryPresenter(View, Repository);
        }
    }








}