<script lang="ts" generics="T">
  import type { Column } from '$lib/components/table/column';

  export let columns: Record<string, Column<T>>;
  export let values: T[] | null | undefined;
</script>

<tbody>
{#each values ?? [] as value}
  <tr class="">
    {#each Object.values(columns) as col}
      <td
        class="max-w-20 px-5 py-2 border-b border-gray-200 bg-white text-sm truncate"
      >
        <slot {value} {col} />
      </td>
    {/each}
  </tr>
{/each}
</tbody>
