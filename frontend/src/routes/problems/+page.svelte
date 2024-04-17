<script lang="ts">
  import { AppRoute } from '$lib/routes';
  import LinkButton from '$lib/components/LinkButton.svelte';
  import type { apiSchemas } from '$lib/api';
  import type { Column } from '$lib/components/table/column';
  import Table from '$lib/components/table/Table.svelte';

  export let data;

  const columns: { [key: string]: Column<apiSchemas['Problem']> } = {
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
  <LinkButton href={AppRoute.PROBLEMS_NEW}>Create new</LinkButton>

  <h2 class="text-xl font-semibold text-gray-800">Problems</h2>

  <div class="mt-4 overflow-x-auto">
    <Table {columns} data={data.value} let:row={problem} let:col={col}>
      {#if col?.key}
        <p
          class="text-gray-900 whitespace-no-wrap overflow-hidden overflow-ellipsis"
          title={problem[col.key]?.toString()}
        >
          {problem[col.key]}
        </p>
      {:else}
        <div class="flex justify-end space-x-1">
          <a href="{AppRoute.PROBLEMS}/{problem.id}"
             class="inline-block bg-gray-700 hover:bg-gray-600 text-white font-medium py-1 px-2 rounded transition duration-300 ease-in-out"
          >
            Edit
          </a>
          <button
            class="bg-red-600 hover:bg-red-500 text-white font-medium py-1 px-2 rounded transition duration-300 ease-in-out"
          >
            Delete
          </button>
        </div>
      {/if}
    </Table>
  </div>
</div>
<!-- TODO: Pagination, filtering, sorting -->
