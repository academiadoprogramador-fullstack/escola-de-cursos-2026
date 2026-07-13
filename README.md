# Escola de Cursos

## Projeto

Desenvolvido durante o curso Fullstack da [Academia do Programador](https://www.academiadoprogramador.net) 2026

## Funcionalidades

1. Módulo de Categoria

   ### Requisitos Funcionais
   - O sistema deve permitir o cadastro de novas categorias
   - O sistema deve permitir a edição de categorias cadastradas
   - O sistema deve permitir excluir categorias cadastradas
   - O sistema deve permitir visualizar categorias cadastradas

   ### Regras de Negócio
   - Campos obrigatórios:
     - Nome (2-100 caracteres)
   - Não pode haver duas categorias com o mesmo nome.

2. Módulo de Cursos

   ### Requisitos Funcionais
   - O sistema deve permitir o cadastro de novos cursos
   - O sistema deve permitir a edição de cursos já cadastrados
   - O sistema deve permitir excluir cursos já cadastrados
   - O sistema deve permitir visualizar cursos cadastrados

   ### Regras de Negócio
   - Campos obrigatórios:
     - Nome (2-100 caracteres)
     - Nivel (Facil, Medio, Dificil)
     - Carga Horária (2-100 Dígitos)
     - Categoria
   - Não pode haver cursos com o mesmo nome.
   - A carga horária de um curso deve ser maior que zero.

3. Módulo de Aulas

   ### Requisitos Funcionais
   - O sistema deve permitir o cadastro de novas aulas
   - O sistema deve permitir a edição de aulas cadastradas
   - O sistema deve permitir excluir aulas cadastradas
   - O sistema deve permitir visualizar aulas cadastradas

   ### Regras de Negócio
   - Campos obrigatórios:
     - Nome (2-100 caracteres)
     - Duração da aula (O valor não pode ser negativo)
     - Ordem (número inteiro para ordenação)
   - A ordem da aula dentro de um curso não poderá ser repetida.
   - Não pode haver duas aulas com o mesmo nome.

4. Módulo de Instrutores

   ### Requisitos Funcionais
   - O sistema deve permitir o cadastro de novos tutores
   - O sistema deve permitir a edição de tutores já cadastrados
   - O sistema deve permitir excluir tutores já cadastrados
   - O sistema deve permitir visualizar tutores cadastrados

   ### Regras de Negócio
   - Campos obrigatórios:
     - Nome (2-100 caracteres)
     - Telefone (formato validado: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX)
     - Email (formato válido)
     - Graduação (2-100 caracteres)
   - Não pode haver tutores com o mesmo nome.

5. Módulo de Turmas

   ### Requisitos Funcionais
   - O sistema deve permitir o cadastro de novas turmas
   - O sistema deve permitir a edição de turmas cadastradas
   - O sistema deve permitir excluir turmas já cadastradas
   - O sistema deve permitir visualizar turmas cadastradas

   ### Regras de Negócio
   - Campos obrigatórios:
     - Nome (2-100 caracteres)
     - Curso (Cada turma deve possuir um curso)
     - Instrutor (Cada turma deve possuir um instrutor)
     - Número máximo de alunos (1-100 Dígitos)
     - Data de Início
     - Data de Término (A data de término deve ser posterior a data de início)
   - Toda turma deve possuir exatamente um instrutor.
   - A data de término da turma deve ser posterior à data de início.
   - O número máximo de alunos em uma turma deve ser maior que zero.

6. Módulo de Alunos

   ### Requisitos Funcionais
   - O sistema deve permitir o cadastro de novos alunos
   - O sistema deve permitir a edição de alunos cadastrados
   - O sistema deve permitir excluir alunos já cadastrados
   - O sistema deve permitir visualizar alunos cadastrados

   ### Regras de Negócio
   - Campos obrigatórios:
     - Nome (2-100 caracteres)
     - Telefone (formato validado: (XX) XXXX-XXXX ou (XX) XXXXX-XXXX)
     - Email (formato válido)
   - Não pode haver alunos com a mesma matrícula.

7. Módulo de Matrícula

   ### Requisitos Funcionais
   - O sistema deve permitir adicionar alunos a uma turma
   - O sistema deve permitir remover alunos de uma turma
   - O sistema deve permitir visualizar todos os alunos de uma turma

   ### Regras de Negócio
   - Campos obrigatórios:
     - Número da Matrícula (Gerada após o cadastro do aluno)
     - Aluno (seleção obrigatória)
     - Curso (Seleção obrigatória)
   - Não pode adicionar o mesmo aluno duas vezes na mesma turma
   - Somente alunos cadastrados poderão ser matriculados.
   - Somente turmas cadastradas poderão receber matrículas.

## Como utilizar

1. Clone o repositório ou baixe o código fonte.
2. Abra o terminal ou o prompt de comando e navegue até a pasta raiz
3. Utilize o comando abaixo para restaurar as dependências do projeto.

   ```bash
   dotnet restore
   ```

4. Para executar o projeto compilando em tempo real

   ```bash
   dotnet run --project EscolaDeCursos.WebApp
   ```

## Requisitos

- .NET 10.0 SDK
