<script lang="ts">
  import { enhance } from '$app/forms';
  import Input from '$lib/components/Input.svelte';
  import Label from '$lib/components/Label.svelte';
  import Button from '$lib/components/Button.svelte';
  import FormCloseButton from '$lib/components/FormCloseButton.svelte';
  import { AppRoute } from '$lib/routes';
  import { difficulties } from '../difficulty';

  export let form;
  export let data;

  let problems = data.problems;
</script>

<div class="bg-gray-100 p-6 w-full max-w-md mx-auto rounded-lg shadow-md relative">
  <h2 class="text-xl font-semibold text-gray-800">Create</h2>

  <FormCloseButton to={AppRoute.ATTEMPTS} />
  <form method="POST" on:submit|preventDefault use:enhance>
    <div>
      <Label for="number">Problem ID</Label>
      <select name="problemId">
        {#each problems as problem}
          <option value={problem.id}>{problem.name}</option>
        {/each}
      </select>
    </div>

    <div>
      <Label for="name">Minutes spent</Label>
      <Input id="minutesSpent" name="minutesSpent" type="number" />
    </div>

    <div>
      <Label for="name">Date</Label>
      <Input name="date" type="date" />
    </div>

    <div>
      <Label for="name">Difficulty</Label>
      <select name="difficulty">
        {#each difficulties as difficulty}
          <option value={difficulty}>{difficulty}</option>
        {/each}
      </select>
    </div>

    <div>
      <Label for="name">Has used help</Label>
      <Input name="hasUsedHelp" type="checkbox" />
    </div>

    <div>
      <Label for="name">Has solved</Label>
      <Input name="hasSolved" type="checkbox" />
    </div>

    <!-- After 1st success, every attempt should automatically be marked as recap on API side TODO -->
    <div>
      <Label for="name">Is recap</Label>
      <Input name="isRecap" type="checkbox" />
    </div>

    <Button type="submit" class="mt-2">Create</Button>
  </form>

  {#if form?.fieldErrors || form?.formErrors}
    <div>
      <ul>
        {#each ([...Object.entries(form.fieldErrors), ...Object.entries(form.formErrors)]).filter(x => x) as error}
          <li class="mt-2 text-sm text-red-500">{error}</li>
        {/each}
      </ul>
    </div>
  {/if}
</div>
