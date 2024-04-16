<script>
  import { cn } from '$lib/components/componentUtils';

  let className = undefined;
  export { className as class };
</script>

<input {...$$restProps}
       class={cn("mt-1 block w-full px-3 py-2 bg-gray-200 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:border-gray-400 focus:bg-white", className)} />
