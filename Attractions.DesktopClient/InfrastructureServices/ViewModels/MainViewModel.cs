using Attractions.ApplicationServices.GetAttractionListUseCase;
using Attractions.ApplicationServices.Ports;
using Attractions.DomainObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Attractions.DesktopClient.InfrastructureServices.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IGetAttractionListUseCase _getAttractionListUseCase;

        public MainViewModel(IGetAttractionListUseCase getAttractionListUseCase)
            => _getAttractionListUseCase = getAttractionListUseCase;

        private Task<bool> _loadingTask;
        private Attraction _currentAttraction;
        private ObservableCollection<Attraction> _attractions;

        public event PropertyChangedEventHandler PropertyChanged;

        public Attraction CurrentAttraction
        {
            get => _currentAttraction; 
            set
            {
                if (_currentAttraction != value)
                {
                    _currentAttraction = value;
                    OnPropertyChanged(nameof(CurrentAttraction));
                }
            }
        }

        private async Task<bool> LoadAttractions()
        {
            var outputPort = new OutputPort();
            bool result = await _getAttractionListUseCase.Handle(GetAttractionListUseCaseRequest.CreateAllAttractionsRequest(), outputPort);
            if (result)
            {
                Attractions = new ObservableCollection<Attraction>(outputPort.Attractions);
            }
            return result;
        }

        public ObservableCollection<Attraction> Attractions
        {
            get 
            {
                if (_loadingTask == null)
                {
                    _loadingTask = LoadAttractions();
                }
                
                return _attractions; 
            }
            set
            {
                if (_attractions != value)
                {
                    _attractions = value;
                    OnPropertyChanged(nameof(Attractions));
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private class OutputPort : IOutputPort<GetAttractionListUseCaseResponse>
        {
            public IEnumerable<Attraction> Attractions { get; private set; }

            public void Handle(GetAttractionListUseCaseResponse response)
            {
                if (response.Success)
                {
                    Attractions = new ObservableCollection<Attraction>(response.Attractions);
                }
            }
        }
    }
}
