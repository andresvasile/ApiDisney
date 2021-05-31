using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ApiDisney.Dto;
using ApiDisney.Errors;
using ApiDisney.Models;
using AutoMapper;
using Interfaces;
using Specifications;

namespace ApiDisney.Controllers
{
    public class CharactersController : BaseApiController
    {
        public IUnitOfWork _unitOfWork { get; set; }
        public IMapper _mapper{ get; set; }

        public CharactersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetCharacters()
        {
            var characters= await _unitOfWork.Repository<Character>().ListAllAsync();

            return _mapper.Map<List<Character>, List<CharacterDto>>(characters.ToList());
        }

        [HttpPost]
        public async Task<ActionResult<Character>> CreateCharacter(CharacterDto characterDto)
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

            return character;

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Character>> UpdateCharacter(int id, [FromBody] CharacterDto characterDto)
        {
            var spec = new ExistingCharacterByIdSpecification(id);

            var character = await _unitOfWork.Repository<Character>().GetEntityWithSpec(spec);

            if (character == null)
            {
                return BadRequest(new ApiResponse(404, "Character not found"));
            }

            character.Name = characterDto.Name ?? character.Name;
            character.Age = characterDto.Age < 0 ? character.Age : characterDto.Age;
            character.Weight = characterDto.Weight <0 ? character.Weight : characterDto.Weight;
            character.History = characterDto.History ?? character.History;
            character.Image = characterDto.Image ?? character.Image;

            var result = await _unitOfWork.Complete();

            if (result <= 0) return null;

            return character;
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

        [HttpGet("name/{name}")]
        public async Task<ActionResult<IReadOnlyList<CharacterDto>>> GetCharacterByNombre(string name)
        {
            if(!string.IsNullOrEmpty(name))
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
        public async Task<ActionResult<CharacterDto>> GetCharacterByAge(int age)
        {
            if (age > 0)
            {
                var spec = new FilterCharacterByAgeSpecification(age);
                var charactersWithAgeSpecific = await _unitOfWork.Repository<Character>().GetEntityWithSpec(spec);

                if (charactersWithAgeSpecific != null)
                {
                    return _mapper.Map<Character, CharacterDto>(charactersWithAgeSpecific);
                }
                return Ok("No se encontraron resultados con esa edad");

            }

            return BadRequest(new ApiResponse(400, "No puede ser negativa la edad"));
        }
    }
}
