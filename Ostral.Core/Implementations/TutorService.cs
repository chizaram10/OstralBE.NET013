using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Ostral.Core.DTOs;
using Ostral.Core.Interfaces;
using Ostral.Core.Results;
using Ostral.Domain.Models;

namespace Ostral.Core.Implementations;

public class  TutorService : ITutorService
{
    private readonly ITutorRepository _tutorRepository;

    public TutorService(ITutorRepository tutorRepository, Cloudinary cloudinary, ICategoryRepository categoryRepository)
    {
        _tutorRepository = tutorRepository;
    }

    public async Task<Result<IEnumerable<TutorDTO>>> GetTutors(int pageSize, int pageNumber)
    {
        var result = await _tutorRepository.GetTutors(pageSize, pageNumber);

        return new Result<IEnumerable<TutorDTO>>
        {
            Success = true,
            Data = result .PageItems!.Select(t => CreateTutorDTO(t))
        };
    }

    public async Task<Result<IEnumerable<TutorDTO>>> GetPopularTutors()
    {
        var result = await _tutorRepository.GetPopularTutors();

		return new Result<IEnumerable<TutorDTO>>
        {
            Success = true,
            Data = result.Select(t => CreateTutorDTO(t))
        };
    }

	public async Task<Result<TutorDTO>> GetTutorById(string id)
	{
		try
		{
			var tutor = await _tutorRepository.GetTutorById(id);
			if (tutor == null) return new Result<TutorDTO>
			{
				Success = false,
				Errors = new string[] { $"Tutor with this id '{id}' not found." }
			};

			return new Result<TutorDTO>
			{
				Data = CreateTutorDTO(tutor),
				Success = true
			};
		}
		catch (Exception ex)
		{
			return new Result<TutorDTO>
			{
				Success = false,
				Errors = (new string[] { ex.Message })
			};
		}
	}

    private static TutorDTO CreateTutorDTO(Tutor tutor)
    {
        return new TutorDTO
        {
            Id = tutor.Id,
            FullName = $"{tutor.User.FirstName} {tutor.User.LastName}",
            ImageUrl = tutor.User.ImageUrl,
            Profession = tutor.Profession,
        };
	}
}