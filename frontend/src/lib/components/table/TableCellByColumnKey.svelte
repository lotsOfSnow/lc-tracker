<script lang="ts" generics="T">
  import type { Column } from '$lib/components/table/column';

  export let value: T;
  export let col: Column<T>;

  const mapValue = (value: unknown) => {
    if (typeof value !== 'boolean') {
      return value;
    }

    return value === true ? 'YES' : 'NO';
  };
</script>

<p
  class="text-gray-900 whitespace-no-wrap overflow-hidden overflow-ellipsis font-semibold"
  title={value[col.key]?.toString()}
>
  <slot>
    {mapValue(value[col.key])}
  </slot>
</p>
