using ApiDisney.Dto;
using ApiDisney.Errors;
using ApiDisney.Models;
using AutoMapper;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Specifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDisney.Controllers
{
    public class CharactersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CharactersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CharacterSpecificDto>>> GetCharacters()
        {
            var characters = await _unitOfWork.Repository<Character>().ListAllAsync();

            return _mapper.Map<List<Character>, List<CharacterSpecificDto>>(characters.ToList());
        }

        [HttpPost]
        public async Task<ActionResult<CharacterDto>> CreateCharacter(CharacterDto characterDto)
        {
            var spec = new ExistingCharacterByNameSpecification(characterDto.Name);

            var characterValidate = await _unitOfWork.Repository<Character>().GetEntityWithSpec(spec);

            if (characterValidate != null)
            {
                return BadRequest(new ApiResponse(400, "Character already exists"));
            }

            var character = _mapper.Map<CharacterDto, Character>(characterDto);

            _unitOfWork.Repository<Character>().Add(character);

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            characterDto.Id_Character = character.Id_Character;
            return characterDto;

        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CharacterDto>> UpdateCharacter(int id, [FromBody] CharacterDto characterDto)
        {
            var spec = new ExistingCharacterByIdSpecification(id);

            var character = await _unitOfWork.Repository<Character>().GetEntityWithSpec(spec);

            if (character == null)
            {
                return BadRequest(new ApiResponse(404, "Character not found"));
            }

            var specName = new ExistingCharacterByNameSpecification(characterDto.Name);

            var characterVlidate = await _unitOfWork.Repository<Character>().GetEntityWithSpec(specName);

            if (characterVlidate != null && character.Id_Character != characterVlidate.Id_Character)
            {
                return BadRequest(new ApiResponse(400, "Character name already exists"));
            }

            character.Name = characterDto.Name ?? character.Name;
            character.Age = characterDto.Age < 0 ? character.Age : characterDto.Age;
            character.Weight = characterDto.Weight < 0 ? character.Weight : characterDto.Weight;
            character.History = characterDto.History ?? character.History;
            character.Image = characterDto.Image ?? character.Image;

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            var characterDtoToReturn = _mapper.Map<Character, CharacterDto>(character);

            return characterDtoToReturn;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCharacter(int id)
        {
            var spec = new ExistingCharacterByIdSpecification(id);

            var character = await _unitOfWork.Repository<Character>().GetEntityWithSpec(spec);

            if (character == null)
            {
                return BadRequest(new ApiResponse(404, "Character not found"));
            }

            _unitOfWork.Repository<Character>().Delete(character);
            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            return Ok($" Character {character.Name} was deleted");
        }


        [HttpGet("details/{idCharacter}")]
        public async Task<ActionResult<CharacterDto>> DetailsCharacter(int idCharacter)
        {
            var characterValidate = await _unitOfWork.Repository<Character>().GetByIdAsync(idCharacter);

            if (characterValidate == null)
            {
                return BadRequest(new ApiResponse(400, "Character don't exists"));
            }

            var characterDto = _mapper.Map<Character, CharacterDto>(characterValidate);

            return characterDto;

        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetCharacterByNombre(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var spec = new FilterCharacterByNameSpecification(name);
                var charactersWithNameSpecific = await _unitOfWork.Repository<Character>().ListAsync(spec);

                if (charactersWithNameSpecific.Count > 0)
                {
                    return _mapper.Map<List<Character>, List<CharacterDto>>(charactersWithNameSpecific.ToList());
                }
                return Ok("No se encontraron resultados con ese nombre");

            }

            return BadRequest(new ApiResponse(400, "No ingreso ningun filtro"));
        }

        [HttpGet("age/{age}")]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetCharacterByAge(int age)
        {
            if (age > 0)
            {
                var spec = new FilterCharacterByAgeSpecification(age);
                var charactersWithAgeSpecific = await _unitOfWork.Repository<Character>().ListAsync(spec);

                if (charactersWithAgeSpecific.Count > 0)
                {
                    return _mapper.Map<List<Character>, List<CharacterDto>>(charactersWithAgeSpecific.ToList());
                }
                return Ok("No se encontraron resultados con esa edad");

            }

            return BadRequest(new ApiResponse(400, "No puede ser negativa la edad"));
        }

        [HttpGet("movies/{idMovie}")]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetCharacterByMovie(int idMovie)
        {
            if (idMovie > 0)
            {
                var spec = new FilterCharactersByMovieSpecification(idMovie);
                var charactersWithIdMovieSpecific = await _unitOfWork.Repository<Character>().ListAsync(spec);

                if (charactersWithIdMovieSpecific.Count > 0)
                {
                    return _mapper.Map<List<Character>, List<CharacterDto>>(charactersWithIdMovieSpecific.ToList());
                }
                return Ok("No se encontraron resultados con ese identificador");

            }

            return BadRequest(new ApiResponse(400, "El identificador de pelicula no puede ser negativo"));
        }

       
    }
}
