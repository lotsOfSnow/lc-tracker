<script lang="ts">
  import Label from '$lib/components/Label.svelte';
  import Input from '$lib/components/Input.svelte';
  import Checkbox from '$lib/components/Checkbox.svelte';
  import type { apiSchemas } from '$lib/api';
  import { difficulties } from '../difficulty';
  import { commonInputControlClassName } from '$lib/components/styleUtils';
  import Button from '$lib/components/Button.svelte';
  import type { ProblemFields } from './attemptUtils';

  export let src: apiSchemas['Attempt'] | undefined = undefined;
  export let problems: ProblemFields[];

  let date = src?.date;

  const setTodayDate = () => {
    date = new Date().toISOString().slice(0, 10);
  };
</script>

<div>
  <Label for="problemId">Problem</Label>
  <select id="problemId" name="problemId" required
          class={commonInputControlClassName}>
    {#each problems as problem}
      <option
        class={commonInputControlClassName} selected={src?.problemId === problem.id}
        value={problem.id}>{problem.title}</option>
    {/each}
  </select>
</div>

<div>
  <Label for="minutesSpent">Minutes spent</Label>
  <Input id="minutesSpent" name="minutesSpent" type="number" value={src?.minutesSpent} />
</div>

<div>
  <Label for="date">Date</Label>
  <div class="flex">
    <Input required id="date" name="date" type="date" bind:value={date} />
    <Button type="button" on:click={setTodayDate}>Today</Button>
  </div>
</div>

<div>
  <Label for="difficulty">Difficulty</Label>
  <select class={commonInputControlClassName} required id="difficulty" name="difficulty">
    {#each difficulties as difficulty}
      <option class={commonInputControlClassName} value={difficulty}
              selected={src?.difficulty === difficulty}>{difficulty}</option>
    {/each}
  </select>
</div>

<div>
  <Label for="hasUsedHelp">Has used help</Label>
  <Checkbox id="hasUsedHelp" title="has used help?" name="hasUsedHelp" checked={src?.hasUsedHelp} />
</div>

<div>
  <Label for="hasSolved">Has solved</Label>
  <Checkbox id="hasSolved" name="hasSolved" type="checkbox" checked={src?.hasSolved} />
</div>

<!-- After 1st success, every attempt should automatically be marked as recap on API side TODO -->
<div>
  <Label for="isRecap">Is recap</Label>
  <Checkbox id="isRecap" name="isRecap" type="checkbox" checked={src?.isRecap} />
</div>

<div>
  <Label for="note">Note</Label>
  <textarea id="note" name="note" class={commonInputControlClassName}>{src?.note || ''}</textarea>
</div>
