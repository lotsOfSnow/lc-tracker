<script lang="ts">
  import { type ClassName, cn } from '$lib/components/componentUtils';
  import type { HTMLInputTypeAttribute } from 'svelte/elements';
  import { commonInputControlClassName } from '$lib/components/styleUtils';

  let className: ClassName = undefined;
  export { className as class };

  export let id: string;
  export let name: string;
  export let type: HTMLInputTypeAttribute;
  export let required: boolean | undefined = undefined;
</script>

<input {...$$restProps} {id} {name} {type} {required}
       class={cn(commonInputControlClassName, className)} />
