<script lang="ts">
  import { enhance } from '$app/forms';
  import Input from '$lib/components/Input.svelte';
  import Label from '$lib/components/Label.svelte';
  import Button from '$lib/components/Button.svelte';
  import FormCloseButton from '$lib/components/FormCloseButton.svelte';
  import { AppRoute } from '$lib/routes';
  import { difficulties } from '../difficulty';
  import { commonInputControlClassName } from '$lib/components/styleUtils';
  import Checkbox from '$lib/components/Checkbox.svelte';

  export let form;
  export let data;

  let problems = data.problems;
</script>

<div class="bg-gray-100 p-6 w-full max-w-md mx-auto rounded-lg shadow-md relative">
  <h2 class="text-xl font-semibold text-gray-800">Create</h2>

  <FormCloseButton to={AppRoute.ATTEMPTS} />
  <form method="POST" on:submit|preventDefault use:enhance>
    <div>
      <Label for="problemId">Problem</Label>
      <select id="problemId" required
              class={commonInputControlClassName}>
        {#each problems as problem}
          <option
            class={commonInputControlClassName}
            value={problem.id}>{problem.name}</option>
        {/each}
      </select>
    </div>

    <div>
      <Label for="minutesSpent">Minutes spent</Label>
      <Input required id="minutesSpent" type="number" />
    </div>

    <div>
      <Label for="date">Date</Label>
      <Input required id="date" type="date" />
    </div>

    <div>
      <Label for="difficulty">Difficulty</Label>
      <select class={commonInputControlClassName} required id="difficulty">
        {#each difficulties as difficulty}
          <option class={commonInputControlClassName} value={difficulty}>{difficulty}</option>
        {/each}
      </select>
    </div>

    <div>
      <Label for="hasUsedHelp">Has used help</Label>
      <Checkbox id="hasUsedHelp" title="has used help?" name="hasUsedHelp" />
    </div>

    <div>
      <Label for="hasSolved">Has solved</Label>
      <Checkbox id="hasSolved" type="checkbox" />
    </div>

    <!-- After 1st success, every attempt should automatically be marked as recap on API side TODO -->
    <div>
      <Label for="isRecap">Is recap</Label>
      <Checkbox id="isRecap" type="checkbox" />
    </div>

    <div>
      <Label for="note">Note</Label>
      <textarea id="note" class={commonInputControlClassName} />
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
