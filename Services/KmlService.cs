using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using VibeApiTestV11ByRvw.Models;
using VibeApiTestV11ByRvw.Services.Interfaces;

namespace VibeApiTestV11ByRvw.Services
{
    public class KmlService : IKmlService
    {
        private readonly string _kmlFilePath = Path.Combine("Data", "DIRECIONADORES1.kml");
        private List<PlacemarkModel> _placemarks;

        public KmlService()
        {
            _placemarks = LoadKmlFile();
        }

        private List<PlacemarkModel> LoadKmlFile()
        {
            var placemarks = new List<PlacemarkModel>();

            using (var stream = File.OpenRead(_kmlFilePath))
            {
                var parser = new Parser();
                parser.Parse(stream);

                var kml = parser.Root as Kml;
                var document = kml.Feature as Document;

                foreach (var placemark in document.Flatten().OfType<Placemark>())
                {
                    var model = new PlacemarkModel
                    {
                        Name = placemark.Name,
                        Description = placemark.Description?.Text,
                        Latitude = ((placemark.Geometry as Point)?.Coordinate.Latitude) ?? 0,
                        Longitude = ((placemark.Geometry as Point)?.Coordinate.Longitude) ?? 0,
                    };

                    // Extração dos dados customizados
                    foreach (var data in placemark.ExtendedData?.Data ?? Enumerable.Empty<Data>())
                    {
                        switch (data.Name.ToUpper())
                        {
                            case "CLIENTE":
                                model.Cliente = data.Value;
                                break;
                            case "SITUAÇÃO":
                            case "SITUACAO":
                                model.Situacao = data.Value;
                                break;
                            case "BAIRRO":
                                model.Bairro = data.Value;
                                break;
                            case "REFERENCIA":
                                model.Referencia = data.Value;
                                break;
                            case "RUA/CRUZAMENTO":
                                model.RuaCruzamento = data.Value;
                                break;
                        }
                    }

                    placemarks.Add(model);
                }
            }

            return placemarks;
        }

        public Task<List<PlacemarkModel>> GetPlacemarksAsync()
        {
            return Task.FromResult(_placemarks);
        }

        public Task<List<PlacemarkModel>> GetFilteredPlacemarksAsync(PlacemarkFilter filter)
        {
            var query = _placemarks.AsQueryable();

            if (filter.Cliente != null && filter.Cliente.Any())
            {
                query = query.Where(p => filter.Cliente.Contains(p.Cliente));
            }

            if (filter.Situacao != null && filter.Situacao.Any())
            {
                query = query.Where(p => filter.Situacao.Contains(p.Situacao));
            }

            if (filter.Bairro != null && filter.Bairro.Any())
            {
                query = query.Where(p => filter.Bairro.Contains(p.Bairro));
            }

            if (!string.IsNullOrEmpty(filter.Referencia))
            {
                query = query.Where(p => p.Referencia != null && p.Referencia.Contains(filter.Referencia, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(filter.RuaCruzamento))
            {
                query = query.Where(p => p.RuaCruzamento != null && p.RuaCruzamento.Contains(filter.RuaCruzamento, StringComparison.OrdinalIgnoreCase));
            }

            return Task.FromResult(query.ToList());
        }

        public Task<byte[]> ExportFilteredKmlAsync(PlacemarkFilter filter)
        {
            var filteredPlacemarks = GetFilteredPlacemarksAsync(filter).Result;

            var document = new Document();

            foreach (var placemarkModel in filteredPlacemarks)
            {
                var placemark = new Placemark
                {
                    Name = placemarkModel.Name,
                    Description = new Description { Text = placemarkModel.Description },
                    Geometry = new Point
                    {
                        Coordinate = new Vector(placemarkModel.Latitude, placemarkModel.Longitude)
                    },
                    ExtendedData = new ExtendedData()
                };

                // Adicionar dados customizados
                if (!string.IsNullOrEmpty(placemarkModel.Cliente))
                {
                    placemark.ExtendedData.AddData(new Data { Name = "CLIENTE", Value = placemarkModel.Cliente });
                }

                if (!string.IsNullOrEmpty(placemarkModel.Situacao))
                {
                    placemark.ExtendedData.AddData(new Data { Name = "SITUAÇÃO", Value = placemarkModel.Situacao });
                }

                if (!string.IsNullOrEmpty(placemarkModel.Bairro))
                {
                    placemark.ExtendedData.AddData(new Data { Name = "BAIRRO", Value = placemarkModel.Bairro });
                }

                if (!string.IsNullOrEmpty(placemarkModel.Referencia))
                {
                    placemark.ExtendedData.AddData(new Data { Name = "REFERENCIA", Value = placemarkModel.Referencia });
                }

                if (!string.IsNullOrEmpty(placemarkModel.RuaCruzamento))
                {
                    placemark.ExtendedData.AddData(new Data { Name = "RUA/CRUZAMENTO", Value = placemarkModel.RuaCruzamento });
                }

                document.AddFeature(placemark);
            }

            var kml = new Kml { Feature = document };
            var serializer = new Serializer();
            serializer.Serialize(kml);

            var kmlBytes = System.Text.Encoding.UTF8.GetBytes(serializer.Xml);

            return Task.FromResult(kmlBytes);
        }

        public Task<AvailableFilters> GetAvailableFiltersAsync()
        {
            var availableFilters = new AvailableFilters
            {
                Cliente = _placemarks.Select(p => p.Cliente).Where(c => !string.IsNullOrEmpty(c)).Distinct().ToList(),
                Situacao = _placemarks.Select(p => p.Situacao).Where(s => !string.IsNullOrEmpty(s)).Distinct().ToList(),
                Bairro = _placemarks.Select(p => p.Bairro).Where(b => !string.IsNullOrEmpty(b)).Distinct().ToList(),
            };


            return Task.FromResult(availableFilters);
        }
    }
}
