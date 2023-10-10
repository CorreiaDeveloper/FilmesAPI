﻿using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmeController : ControllerBase
{

    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um filme ao Banco de Dados
    /// </summary>
    /// <param name="filmeDto">Objeto com os campos necessários para criação de um filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarFilme([FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof (RecuperaFilmePorId), new { id = filme.Id}, filme);
    }

    /// <summary>
    /// Lista os filmes adicionados ao Banco de Dados
    /// </summary>
    /// <returns>IEnumerable</returns>
    /// <response code="200">Caso a listagem seja feita com sucesso</response>
    [HttpGet]
    public IEnumerable<ReadFilmeDto> RecuperaFilmes([FromQuery] int skip = 0, 
        [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take));
    }

    /// <summary>
    /// Lista um filme especifíco do Banco de Dados
    /// </summary>
    /// <param name="id">campo necessário para listagem filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso listagem seja feita com sucesso</response>
    [HttpGet("{id}")]
    public IActionResult? RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
        return Ok(filmeDto);
    }


    /// <summary>
    /// Atualiza os dados de um filme especifíco do Banco de Dados
    /// </summary>
    /// <param name="id">campo necessário para seleção do filme</param>
    /// <param name="filmeDto">Objeto com o campo necessário para atualizar os dados de qualquer filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso atualização de dados seja feita com sucesso</response>
    [HttpPut("{id}")]
    public IActionResult AtualizarFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        _mapper.Map(filmeDto, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Atualiza os dados parciais de um filme especifíco do Banco de Dados
    /// </summary>
    /// <param name="id">campo necessário para seleção do filme</param>
    /// <param name="patch">Objeto com o campo necessário para atualizar os dados de qualquer filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso atualização de dados parciais seja feita com sucesso</response>
    [HttpPatch("{id}")]
    public IActionResult AtualizarFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        var filmeParaAtualizar = _mapper.Map<UpdateFilmeDto>(filme);
        patch.ApplyTo(filmeParaAtualizar, ModelState);

        if (!TryValidateModel(filmeParaAtualizar))
        {
            return ValidationProblem(ModelState);
        }

        _mapper.Map(filmeParaAtualizar, filme);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Exclui um filme do Banco de Dados
    /// </summary>
    /// <param name="id">campo necessário para seleção do filme</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso a exclusão seja feita com sucesso</response>
    [HttpDelete("{id}")]
    public IActionResult DeletaFilme(int id)
    {
        var filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();

        _context.Remove(filme);
        _context.SaveChanges();
        return NoContent();
    }
}