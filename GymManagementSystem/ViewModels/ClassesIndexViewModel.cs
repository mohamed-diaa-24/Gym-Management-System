using GymManagementSystem.Models;

namespace GymManagementSystem.ViewModels;

public class ClassesIndexViewModel
{
    public IEnumerable<GymClass> Classes { get; set; }
    public IEnumerable<Trainer> Trainers { get; set; }
    public int? SelectedTrainerId { get; set; }
    public string SearchTerm { get; set; }
}