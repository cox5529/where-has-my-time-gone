<script setup lang="ts">
import SectionHeader from './typography/SectionHeader.vue';
import { computed } from 'vue';

import moment from 'moment';

import type { Person } from '@/models/person';

const props = defineProps<{ person: Person }>();

const sum = computed(() =>
  props.person.usages.reduce(
    (prev, cur) => prev + (cur.durationSeconds ?? 0),
    0
  )
);

function renderDate(isoDate?: string): string {
  if (!isoDate) {
    return '';
  }

  return moment(isoDate).format('llll');
}
</script>

<template>
  <div class="bg-slate-700 rounded-md p-8">
    <SectionHeader>Usages for {{ props.person.name }}</SectionHeader>
    <table class="w-full">
      <thead>
        <tr class="font-semibold border-b">
          <td>Start</td>
          <td>End</td>
          <td>Duration (seconds)</td>
        </tr>
      </thead>
      <tbody>
        <tr v-for="usage in props.person.usages" :key="usage.id">
          <td>{{ renderDate(usage.start) }}</td>
          <td>{{ renderDate(usage.end) }}</td>
          <td>{{ usage.durationSeconds }}</td>
        </tr>
      </tbody>
      <tfoot>
        <tr>
          <td></td>
          <td class="font-semibold text-right pr-4">Total</td>
          <td>{{ sum }}</td>
        </tr>
      </tfoot>
    </table>
  </div>
</template>
