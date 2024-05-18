<script lang="ts">
  import { type ClassName, cn } from '$lib/components/componentUtils';
  import type { HTMLInputTypeAttribute } from 'svelte/elements';
  import { commonInputControlClassName } from '$lib/components/styleUtils';

  let className: ClassName = undefined;
  export { className as class };

  export let id: string;
  export let name: string | undefined = undefined;
  export let type: HTMLInputTypeAttribute;
  export let required: boolean | undefined = undefined;
  export let disabled: boolean | undefined = false;
  export let value: string | number | null | undefined = '';


</script>

<input {...$$restProps} {id} {name} {required} bind:value {...{ type }} {disabled}
       class={cn(commonInputControlClassName, className)} />
