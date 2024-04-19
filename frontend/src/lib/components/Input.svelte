<script lang="ts">
  import { type ClassName, cn } from '$lib/components/componentUtils';
  import type { HTMLInputTypeAttribute } from 'svelte/elements';

  let className: ClassName = undefined;
  export { className as class };

  export let name: string;
  export let type: HTMLInputTypeAttribute;
  export let required: boolean | undefined = undefined;
</script>

<input {...$$restProps} {name} {type} {required}
       class={cn("mt-1 block w-full px-3 py-2 bg-gray-200 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:border-gray-400 focus:bg-white", className)} />
