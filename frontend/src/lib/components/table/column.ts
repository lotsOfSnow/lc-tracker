export interface Column<schema> {
  key?: keyof schema;
  title: string;
  content?: string;
}
