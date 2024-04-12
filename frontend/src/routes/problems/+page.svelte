<script lang="ts">

  import { AppRoute } from '$lib/routes';
  import type { components } from '$lib/api';
  import LinkButton from '$lib/components/LinkButton.svelte';

  export let data;

  interface Column {
    key: keyof components['schemas']['Problem'],
    title: string,
  }

  const columns: { [key: string]: Column } = {
    number: {
      key: 'number',
      title: 'Number',
    },
    name: {
      key: 'name',
      title: 'Name',
    },
    url: {
      key: 'url',
      title: 'Url',
    },
  };
</script>

<LinkButton href={AppRoute.PROBLEMS_NEW}>
  Create new
</LinkButton>


<table class="w-full">
  <thead class="bg-gray-300 text-left border-b border-neutral-300">
  <tr>
    {#each Object.values(columns) as col}
      <th class="p-2 first:rounded-tl-lg last:rounded-tr-lg">{col.title}</th>
    {/each}
  </tr>
  </thead>
  <tbody>
  {#each data.problems as problem}
    <tr
      class="transition duration-300 ease-in-out hover:bg-neutral-300 border-b border-neutral-200 hover:cursor-pointer">
      {#each Object.values(columns) as col}
        <td class="p-2">{problem[col.key]}</td>
      {/each}
    </tr>
  {/each}
  </tbody>
</table>
