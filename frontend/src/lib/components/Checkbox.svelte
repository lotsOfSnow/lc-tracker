<script lang="ts">
  import { type ClassName, cn } from '$lib/components/componentUtils';

  let className: ClassName = undefined;
  export { className as class };

  export let id: string;
</script>

<input {...$$restProps} {id} type="checkbox"
       class={cn("h-6 w-6 text-gray-600 border-gray-300 rounded focus:ring-gray-500 focus:border-gray-500 bg-gray-200 hover:border-gray-400 focus:ring-offset-0", className)} />
