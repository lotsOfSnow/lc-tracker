<script lang="ts">

  import { AppRoute } from '$lib/routes';
  import type { components } from '$lib/api';
  import LinkButton from '$lib/components/LinkButton.svelte';

  export let data;

  interface Column {
    key?: keyof components['schemas']['Problem'],
    title: string,
    content?: string,
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
    actions: {
      title: 'Actions',
    },
  };
</script>


<div class="bg-gray-100 p-6 w-full max-w-6xl mx-auto rounded-lg shadow-md">
  <LinkButton href={AppRoute.PROBLEMS_NEW}>
    Create new
  </LinkButton>

  <h2 class="text-xl font-semibold text-gray-800">Problems</h2>

  <div class="mt-4 overflow-x-auto">
    <table class="min-w-full leading-normal">
      <thead>
      <tr>
        {#each Object.values(columns) as col}
          <th
            class="first:rounded-tl-lg last:rounded-tr-lg px-5 py-3 border-b-2 border-gray-300 bg-gray-200 text-left text-xs font-semibold text-gray-600 uppercase tracking-wider">{col.title}</th>
        {/each}
      </tr>
      </thead>
      <tbody>
      {#each data.problems as problem}
        <tr class="">
          {#each Object.values(columns) as col}
            <td
              class="max-w-20 px-5 py-2 border-b border-gray-200 bg-white text-sm truncate">
              {#if col.key}
                <p class="text-gray-900 whitespace-no-wrap overflow-hidden overflow-ellipsis"
                   title={problem[col.key]?.toString()}>
                  {problem[col.key]}
                </p>
              {:else}
                <div class="flex justify-end space-x-1">
                  <button
                    class="bg-gray-700 hover:bg-gray-600 text-white font-medium py-1 px-2 rounded transition duration-300 ease-in-out">
                    Edit
                  </button>
                  <button
                    class="bg-red-600 hover:bg-red-500 text-white font-medium py-1 px-2 rounded transition duration-300 ease-in-out">
                    Delete
                  </button>
                </div>
              {/if}
            </td>
          {/each}
        </tr>
      {/each}
      </tbody>
    </table>
  </div>
</div>
